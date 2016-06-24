using System;
using System.IO;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace template_P3 {

public class Shader
{
	// data members
	public int programID, vsID, fsID;
	public int attribute_vpos;
	public int attribute_vnrm;
	public int attribute_vuvs;
	public int uniform_mview;
    public int uniform_viewdirection;
    public int uniform_light1pos;
    public int uniform_light1dif;
    public int uniform_light1spec;
    public int uniform_light1att;
    public int uniform_light2pos;
    public int uniform_light2dif;
    public int uniform_light2spec;
    public int uniform_light2att;
    public int uniform_light3pos;
    public int uniform_light3dif;
    public int uniform_light3spec;
    public int uniform_light3att;
    public int uniform_light4pos;
    public int uniform_light4dif;
    public int uniform_light4spec;
    public int uniform_light4att;

	// constructor
	public Shader( String vertexShader, String fragmentShader )
	{
		// compile shaders
		programID = GL.CreateProgram();
		Load( vertexShader, ShaderType.VertexShader, programID, out vsID );
		Load( fragmentShader, ShaderType.FragmentShader, programID, out fsID );
		GL.LinkProgram( programID );
		Console.WriteLine( GL.GetProgramInfoLog( programID ) );

		// get locations of shader parameters
		attribute_vpos = GL.GetAttribLocation( programID, "vPosition" );
		attribute_vnrm = GL.GetAttribLocation( programID, "vNormal" );
		attribute_vuvs = GL.GetAttribLocation( programID, "vUV" );
		uniform_mview = GL.GetUniformLocation( programID, "transform" );
        uniform_viewdirection = GL.GetUniformLocation( programID, "viewDirection" );

        uniform_light1pos = GL.GetUniformLocation(programID, "light1pos");
        uniform_light1dif = GL.GetUniformLocation(programID, "light1dif");
        uniform_light1spec = GL.GetUniformLocation(programID, "light1spec");
        uniform_light1att = GL.GetUniformLocation(programID, "light1att");

        uniform_light2pos = GL.GetUniformLocation(programID, "light2pos");
        uniform_light2dif = GL.GetUniformLocation(programID, "light2dif");
        uniform_light2spec = GL.GetUniformLocation(programID, "light2spec");
        uniform_light2att = GL.GetUniformLocation(programID, "light2att");

        uniform_light3pos = GL.GetUniformLocation(programID, "light3pos");
        uniform_light3dif = GL.GetUniformLocation(programID, "light3dif");
        uniform_light3spec = GL.GetUniformLocation(programID, "light3spec");
        uniform_light3att = GL.GetUniformLocation(programID, "light3att");

        uniform_light4pos = GL.GetUniformLocation(programID, "light4pos");
        uniform_light4dif = GL.GetUniformLocation(programID, "light4dif");
        uniform_light4spec = GL.GetUniformLocation(programID, "light4spec");
        uniform_light4att = GL.GetUniformLocation(programID, "light4att");
	}

	// loading shaders
	void Load( String filename, ShaderType type, int program, out int ID )
	{
		// source: http://neokabuto.blogspot.nl/2013/03/opentk-tutorial-2-drawing-triangle.html
		ID = GL.CreateShader( type );
		using (StreamReader sr = new StreamReader( filename )) GL.ShaderSource( ID, sr.ReadToEnd() );
		GL.CompileShader( ID );
		GL.AttachShader( program, ID );
		Console.WriteLine( GL.GetShaderInfoLog( ID ) );
	}
}

} // namespace template_P3
