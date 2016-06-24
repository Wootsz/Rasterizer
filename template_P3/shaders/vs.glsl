#version 330
 
// shader input
in vec2 vUV;				// vertex uv coordinate
in vec3 vNormal;			// untransformed vertex normal
in vec3 vPosition;			// untransformed vertex position

// shader output
out vec4 normal;			// transformed vertex normal
out vec2 uv;				
uniform mat4 transform;
out vec3 vertexpos;

// light variables
uniform vec4 light1pos;
uniform vec4 light1dif;
uniform vec4 light1spec;
uniform vec3 light1att;

uniform vec4 light2pos;
uniform vec4 light2dif;
uniform vec4 light2spec;
uniform vec3 light2att;

uniform vec4 light3pos;
uniform vec4 light3dif;
uniform vec4 light3spec;
uniform vec3 light3att;

uniform vec4 light4pos;
uniform vec4 light4dif;
uniform vec4 light4spec;
uniform vec3 light4att;

// vertex shader
void main()
{
	// transform vertex using supplied matrix
	gl_Position = transform * vec4(vPosition, 1.0);

	// forward normal and uv coordinate; will be interpolated over triangle
	normal = transform * vec4( vNormal, 0.0f );
	uv = vUV;

	// the position of the vertex, to give to the fragment shader
	vertexpos = vec3(transform * vec4(vPosition, 1.0));
}