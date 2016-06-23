#version 330
 
// shader input
in vec2 uv;						// interpolated texture coordinates
in vec4 normal;					// interpolated normal
uniform sampler2D pixels;		// texture sampler

// input according to the https://en.wikibooks.org/wiki/GLSL_Programming/GLUT/Smooth_Specular_Highlights 
varying vec4 position;					// position of the vertex (and fragment) in world space
varying vec3 varyingNormalDirection;	// surface normal vector in world space
uniform mat4 v_inv;

// shader output
out vec4 outputColor;

//light source constructor
struct light
{
	vec4 position;
	vec4 diffuse;
	vec4 spelular;
	float constantAttenuation, linearAttenuation, quadraticAttenuation;
	float spotCutoff, spotExponent;
	vec3 spotDirection;
}

//making a new light source
light light0 = light(
	vec4(0.0, 1.0, 2.0, 1.0),
	vec4(1.0, 1.0, 1.0, 1.0),
	vec4(1.0, 1.0, 1.0, 1.0),
	0.0, 1.0, 0.0,
	180.0, 0.0,
	vec3(0.0, 0.0, 0.0)
);

//ambient lighting
vec4 scene_ambient = vec4(0.2, 0.2, 0.2, 1.0);

// materials constructor
struct material
{
	vec4 ambient;
	vec4 diffuse;
	vec4 specular;
	float shininess;
}

// making a new material
material frontMaterial = material(
	vec4(0.2, 0.2, 0.2, 1.0),
	vec4(1.0, 0.8, 0.8, 1.0),
	vec4(1.0, 1.0, 1.0, 1.0),
	5.0
);

// fragment shader
void main()
{
	//!!! sommige onderstaande variabelen bestaan niet!!
	vec3 normalDirection = normalize(varyingNormalDirection);
	vec3 viewDirection = normalize(vec3(v_inv * vec4(0.0, 0.0, 0.0, 0.1) - position));
	vec3 lightDirection;
	float attenuation;

	vec3 positionToLightSource = vec3(light0.position - position);
	float distance = length(positionToLightSource);
	lightDirection = normalize(positionToLightSource);
	attenuation = 1.0 / (light0.constantAttenuation + light0.linearAttenuation * distance + light0.quadraticAttenuation * distance * distance);

	// calculate total ambient lighting
	vec3 ambientLighting = vec3(scene_ambient) * vec3(frontMaterial.ambient);

	// calculate total diffuse reflection
	vec3 diffuseReflection = attenuation * vec3(light0.diffuse) * vec3(frontMaterial.diffuse) * max(0.0, dot(normalDirection, lightDirection));

	// calculate the specula reflection
	vec3 specularReflection;
	if (dot(normalDirection, lightDirection) < 0.0)
	{
		specularReflection = vec3(0.0, 0.0, 0.0);
	}
	else
	{
		specularReflection = attenuation * vec3(light0.specular) * vec3(frontMaterial.specular) 
			* pow(max(0.0, dot(reflect(-lightDirection, normalDirection), viewDirection)), frontMaterial.shininess);
	}

	// return the output collor
    outputColor = vec4(ambientLighting + diffuseReflection + specularReflection, 1.0);
}