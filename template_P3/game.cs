using System.Diagnostics;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;

// minimal OpenTK rendering framework for UU/INFOGR
// Jacco Bikker, 2016

namespace template_P3 {

class Game
{
	// member variables
	public Surface screen;					// background surface for printing etc.
	Mesh mesh, floor;						// a mesh to draw using OpenGL
	const float PI = 3.1415926535f;			// PI
	float a = 0;							// teapot rotation angle
	Stopwatch timer;						// timer for measuring frame duration
	Shader shader;							// shader to use for rendering
	Shader postproc;						// shader to use for post processing
	Texture wood;							// texture to use for rendering
	RenderTarget target;					// intermediate render target
	ScreenQuad quad;						// screen filling quad for post processing
	bool useRenderTarget = true;
    List<Mesh> meshes;
    SceneGraph sceneGraph;
    Camera camera;

	// initialize
	public void Init()
	{
        // make the meshes list and scenegraph and camera
        sceneGraph = new SceneGraph();
        meshes = new List<Mesh>();
        camera = new Camera();
		// load teapot
		mesh = new Mesh( "../../assets/teapot.obj" );
		floor = new Mesh( "../../assets/floor.obj" );
        // add the meshes to the hierarchy
        sceneGraph.AddParent(floor);
        sceneGraph.AddChild(floor, mesh);
        meshes.Add(mesh);
        meshes.Add(floor);
		// initialize stopwatch
		timer = new Stopwatch();
		timer.Reset();
		timer.Start();
		// create shaders
		shader = new Shader( "../../shaders/vs.glsl", "../../shaders/fs.glsl" );
		postproc = new Shader( "../../shaders/vs_post.glsl", "../../shaders/fs_post.glsl" );
		// load a texture
		wood = new Texture( "../../assets/wood.jpg" );
		// create the render target
		target = new RenderTarget( screen.width, screen.height );
		quad = new ScreenQuad();
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
	
		// prepare matrix for vertex shader
		//Matrix4 transform = Matrix4.CreateFromAxisAngle( new Vector3( 0, 1, 0 ), a );
		//transform *= Matrix4.CreateTranslation( 0, -4, -15 );
		Matrix4 transform = Matrix4.CreatePerspectiveFieldOfView( 1.2f, 1.3f, .1f, 1000 );

		// update rotation
		a += 0.001f * frameDuration; 
		if (a > 2 * PI) 
            a -= 2 * PI;

		if (useRenderTarget)
		{
			// enable render target
			target.Bind();

			// render scene to render target
            foreach (Mesh m in meshes)
                sceneGraph.Render(shader, camera.cameramatrix * transform, wood, m);

			// render quad
			target.Unbind();
			quad.Render( postproc, target.GetTextureID() );
		}
		else
		{
			// render scene directly to the screen
            foreach (Mesh m in meshes)
                sceneGraph.Render(shader, transform, wood, m);
		}
	}
}

} // namespace template_P3