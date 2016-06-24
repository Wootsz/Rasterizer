using System;
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
    Light light1, light2, light3, light4;   // the lights
	const float PI = 3.1415926535f;			// PI
	float a = 0;							// teapot rotation angle
	Stopwatch timer;						// timer for measuring frame duration
	Shader shader;							// shader to use for rendering
	Shader postproc;						// shader to use for post processing
	Texture wood;							// texture to use for rendering
	RenderTarget target;					// intermediate render target
	ScreenQuad quad;						// screen filling quad for post processing

    List<Mesh> meshes;                      // list with all the parent meshes on the highest layer that need to be rendered
    SceneGraph sceneGraph;                  // new scenegraph for storing the class hierarchy  
    Camera camera;                          // new camera, for ... looking around
    int t = 4;                              // amount of lights that are on
    bool holdingTab;                        // speaks for itself i think. used for the light on/off-ness

	// initialize
	public void Init()
	{
        // make the meshes list and scenegraph and camera
        sceneGraph = new SceneGraph();
        meshes = new List<Mesh>();
        camera = new Camera();
        // loading the lights
        light1 = new Light(new Vector4(100.0f, 11.0f, 12.0f, 1.0f), Vector4.Zero, Vector4.Zero, Vector3.Zero);
        light2 = new Light(new Vector4(-11.0f, 11.0f, 12.0f, 1.0f), Vector4.Zero, Vector4.Zero, Vector3.Zero);
        light3 = new Light(new Vector4(-20.0f, -30.0f, 2.0f, 1.0f), Vector4.Zero, Vector4.Zero, Vector3.Zero);
        light4 = new Light(new Vector4(0.0f, 1.0f, 2.0f, 1.0f), Vector4.Zero, Vector4.Zero, Vector3.Zero);
		// load teapot and floor
		mesh = new Mesh( "../../assets/teapot.obj" );
		floor = new Mesh( "../../assets/floor.obj" );
        // add the meshes to the hierarchy and the meshes list
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

        // code for turning the lights on / off
        // get the keyboard state
        var keyboard = OpenTK.Input.Keyboard.GetState();
        // make sure you can't hold tab and change t multiple times
        if (!keyboard[OpenTK.Input.Key.Tab])
            holdingTab = false;
        if (keyboard[OpenTK.Input.Key.Tab] && !holdingTab)
        {
            holdingTab = true;
            t++;
        }

        // reset t to 0 when you press tab after 4 lights are on
        if (t > 4)
            t = 0;

        // turn on / off certain lights based on t
        if (t > 0)
        {
            light1.diffuse = new Vector4(11.0f, 11.0f, 11.0f, 11.0f);
            light1.specularity = new Vector4(11.0f, 11.0f, 11.0f, 11.0f);
            light1.attenuation = new Vector3(10.3f, 11.0f, 10.2f);
        }
        else { light1.diffuse = Vector4.Zero; light1.specularity = Vector4.Zero; light1.attenuation = Vector3.Zero; }

        if (t > 1)
        {
            light2.diffuse = new Vector4(11.0f, 11.0f, 11.0f, 11.0f);
            light2.specularity = new Vector4(11.0f, 11.0f, 11.0f, 11.0f);
            light2.attenuation = new Vector3(10.3f, 11.0f, 10.2f);
        }
        else { light2.diffuse = Vector4.Zero; light2.specularity = Vector4.Zero; light2.attenuation = Vector3.Zero; }

        if (t > 2)
        {
            light3.diffuse = new Vector4(11.0f, 11.0f, 11.0f, 11.0f);
            light3.specularity = new Vector4(11.0f, 11.0f, 11.0f, 11.0f);
            light3.attenuation = new Vector3(10.3f, 11.0f, 10.2f);
        }
        else { light3.diffuse = Vector4.Zero; light3.specularity = Vector4.Zero; light3.attenuation = Vector3.Zero; }

        if (t > 3)
        {
            light4.diffuse = new Vector4(11.0f, 11.0f, 11.0f, 11.0f);
            light4.specularity = new Vector4(11.0f, 11.0f, 11.0f, 11.0f);
            light4.attenuation = new Vector3(10.3f, 11.0f, 10.2f);
        }
        else { light4.diffuse = Vector4.Zero; light4.specularity = Vector4.Zero; light4.attenuation = Vector3.Zero; }
    }

	// tick for OpenGL rendering code
	public void RenderGL()
	{
		// measure frame duration
		float frameDuration = timer.ElapsedMilliseconds;
		timer.Reset();
		timer.Start();
	
		// prepare matrix for vertex shader
		Matrix4 transform = Matrix4.CreatePerspectiveFieldOfView( 1.2f, 1.3f, .1f, 1000 );

		// update rotation
		a += 0.001f * frameDuration; 
		if (a > 2 * PI) 
            a -= 2 * PI;

        // enable render target
        target.Bind();

        // render scene to render target
        Matrix4 cameraM = Matrix4.CreateFromAxisAngle(new Vector3(0, 1, 0), a) * camera.cameramatrix * transform;
        foreach (Mesh m in meshes)
            sceneGraph.Render(shader, cameraM, wood, m);

        GL.UseProgram(shader.programID);
        GL.Uniform3(shader.uniform_viewdirection, new Vector3(cameraM.M13, cameraM.M23, cameraM.M33));

        //GL.Uniform4(shader.uniform_light1pos, light1.position);
        //GL.Uniform4(shader.uniform_light1dif, light1.diffuse);
        //GL.Uniform4(shader.uniform_light1spec, light1.specularity);
        //GL.Uniform3(shader.uniform_light1att, light1.attenuation);

        //GL.Uniform4(shader.uniform_light2pos, light2.position);
        //GL.Uniform4(shader.uniform_light2dif, light2.diffuse);
        //GL.Uniform4(shader.uniform_light2spec, light2.specularity);
        //GL.Uniform3(shader.uniform_light2att, light2.attenuation);

        //GL.Uniform4(shader.uniform_light3pos, light3.position);
        //GL.Uniform4(shader.uniform_light3dif, light3.diffuse);
        //GL.Uniform4(shader.uniform_light3spec, light3.specularity);
        //GL.Uniform3(shader.uniform_light3att, light3.attenuation);

        //GL.Uniform4(shader.uniform_light4pos, light4.position);
        //GL.Uniform4(shader.uniform_light4dif, light4.diffuse);
        //GL.Uniform4(shader.uniform_light4spec, light4.specularity);
        //GL.Uniform3(shader.uniform_light4att, light4.attenuation);

        // render quad
        target.Unbind();
        quad.Render(postproc, target.GetTextureID());
	}
}

} // namespace template_P3