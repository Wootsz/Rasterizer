using System.Diagnostics;
using OpenTK;
using System;
using System.Collections.Generic;
using template_P3;

// minimal OpenTK rendering framework for UU/INFOGR
// Jacco Bikker, 2016

namespace template_P3 {

class Game
{
	// member variables
	public static Surface screen;					// background surface for printing etc.
	Mesh mesh, floor;		        		// a mesh to draw using OpenGL
	const float PI = 3.1415926535f;			// PI
	float a = 0, b=0;							// teapot rotation angle
	Stopwatch timer;						// timer for measuring frame duration
	Shader shader;							// shader to use for rendering
	Texture wood;							// texture to use for rendering
    SceneGraph sceneGraph;
    Camera camera;

	// initialize
	public void Init()
	{
        sceneGraph = new SceneGraph();
        camera = new Camera(new Vector3(2, 2, 2), new Vector3(2, 2, 2));
		// load teapot
		mesh = new Mesh( "../../assets/teapot.obj" );
		floor = new Mesh( "../../assets/floor.obj" );

        floor.modelView = new Matrix4(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);
        mesh.modelView = new Matrix4(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);

        sceneGraph.AddParent(floor);
        sceneGraph.AddChild(floor, mesh);
		// initialize stopwatch
		timer = new Stopwatch();
		timer.Reset();
		timer.Start();
		// create shaders
		shader = new Shader( "../../shaders/vs.glsl", "../../shaders/fs.glsl" );
		// load a texture
		wood = new Texture( "../../assets/wood.jpg" );
	}

	// tick for background surface
	public void Tick()
	{
		screen.Clear( 0 );
        camera.HandleInput();
	}

	// tick for OpenGL rendering code
	public void RenderGL()
	{
		// measure frame duration
		float frameDuration = timer.ElapsedMilliseconds;
		timer.Reset();
		timer.Start();
	
		//prepare matrix for vertex shader
        Matrix4 transform = Matrix4.CreateFromAxisAngle(new Vector3(0, 1, b), a);
        transform *= Matrix4.CreateTranslation( 0, -4, -15 );
        transform *= Matrix4.CreatePerspectiveFieldOfView( 1.2f, 1.3f, .1f, 1000 );

        //Matrix4 transform;
        //transform = Matrix4.CreateFromAxisAngle(new Vector3(0, 1, b), a) * camera.Mcamera() * Matrix4.CreatePerspectiveFieldOfView( 1.2f, 1.3f, .1f, 1000 );

		// update rotation
        a -= 0.001f * frameDuration; 
		if (a > 2 * PI) a -= 2 * PI;

		// render scene
        foreach (Mesh m in sceneGraph.children.Keys)
            sceneGraph.Render( shader, transform, wood, m);
	}
}

} // namespace template_P3