using System.Diagnostics;
using OpenTK;
using System;
using System.Collections.Generic;

// minimal OpenTK rendering framework for UU/INFOGR
// Jacco Bikker, 2016

namespace Template_P3 {

class Game
{
	// member variables
	public Surface screen;					// background surface for printing etc.
	Mesh mesh, floor;						// a mesh to draw using OpenGL
	const float PI = 3.1415926535f;			// PI
	float a = 0;							// teapot rotation angle
	Stopwatch timer;						// timer for measuring frame duration
	Shader shader;							// shader to use for rendering
	Texture wood;							// texture to use for rendering
    List<Mesh> meshes;                      // list with all the meshes in the world

	// initialize
	public void Init()
	{
        // initialize meshes list
        meshes = new List<Mesh>();
		// load teapot
		mesh = new Mesh( "../../assets/teapot.obj" );
		floor = new Mesh( "../../assets/floor.obj" );
        floor.children = new List<Mesh>();
        floor.children.Add(mesh);
        meshes.Add(floor);
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
		screen.Print( "hitler was right", 2, 2, 0xffff00 );
	}

	// tick for OpenGL rendering code
	public void RenderGL()
	{
		// measure frame duration
		float frameDuration = timer.ElapsedMilliseconds;
		timer.Reset();
		timer.Start();
	
		// prepare matrix for vertex shader
		Matrix4 transform = Matrix4.CreateFromAxisAngle( new Vector3( 0, 1, 0 ), a );
		transform *= Matrix4.CreateTranslation( 0, -4, -15 );
		transform *= Matrix4.CreatePerspectiveFieldOfView( 1.2f, 1.3f, .1f, 1000 );

		// update rotation
		a += 0.001f * frameDuration; 
		if (a > 2 * PI) a -= 2 * PI;

		// render scene
		SceneGraph.Render( shader, transform, wood, mesh);
        SceneGraph.Render( shader, transform, wood, floor);

	}
}

} // namespace Template_P3