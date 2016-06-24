#version 330
 
// shader input
in vec2 uv;						// interpolated texture coordinates
in vec4 normal;					// interpolated normal
uniform sampler2D pixels;		// texture sampler
in vec3 vertexpos;				// position of the vertex
uniform vec3 viewDirection;		// viewDirection

// light variables
//uniform vec4 light1pos;
//uniform vec4 light1dif;
//uniform vec4 light1spec;
//uniform vec3 light1att;

//uniform vec4 light2pos;
//uniform vec4 light2dif;
//uniform vec4 light2spec;
//uniform vec3 light2att;

//uniform vec4 light3pos;
//uniform vec4 light3dif;
//uniform vec4 light3spec;
//uniform vec3 light3att;

//uniform vec4 light4pos;
//uniform vec4 light4dif;
//uniform vec4 light4spec;
//uniform vec3 light4att;

// shader output
out vec4 outputColor;

//light source constructor
struct lightSource
{
  vec4 position;
  vec4 diffuse;
  vec4 specularity;
  float constantAttenuation, linearAttenuation, quadraticAttenuation;
};

//making the light sources
lightSource light1 = lightSource(vec4(5.0f, 0.0f, 5.0f, 1.0f), vec4(11.0f, 11.0f, 11.0f, 11.0f), vec4(11.0f, 11.0f, 11.0f, 11.0f), 10.3f, 11.0f, 10.2f);
lightSource light2 = lightSource(vec4(-5.0f, 0.0f, 5.0f, 1.0f), vec4(11.0f, 11.0f, 11.0f, 11.0f), vec4(11.0f, 11.0f, 11.0f, 11.0f), 10.3f, 11.0f, 10.2f);
lightSource light3 = lightSource(vec4(-5.0f, 0.0f, -5.0f, 1.0f), vec4(11.0f, 11.0f, 11.0f, 11.0f), vec4(11.0f, 11.0f, 11.0f, 11.0f), 10.3f, 11.0f, 10.2f);
lightSource light4 = lightSource(vec4(5.0f, 0.0f, 5f, 1.0f), vec4(11.0f, 11.0f, 11.0f, 11.0f), vec4(11.0f, 11.0f, 11.0f, 11.0f), 10.3f, 11.0f, 10.2f);

// materials constructor
struct material
{
  vec4 ambient;
  vec4 diffuse;
  vec4 specularity;
  float shininess;
};

//making a new material
material frontMaterial = material(
  vec4(1.0, 1.0, 1.0, 1.0),
  vec4(20.0, 20.0, 20.0, 10.0),
  vec4(20.0, 20.0, 20.0, 20.0),
  20.0
);

// set the ambient color
vec4 scene_ambient = vec4(0.5, 0.5, 0.5, 1.0);

// fragment shader
void main()
{

  vec3 normalDirection = vec3(normal);

  vec3 lightDirection1;
  vec3 lightDirection2;
  vec3 lightDirection3;
  vec3 lightDirection4;

  float attenuation1;
  float attenuation2;
  float attenuation3;
  float attenuation4;

  vec3 positionToLightSource1 = vec3(light1.position - vertexpos);
  vec3 positionToLightSource2 = vec3(light2.position - vertexpos);
  vec3 positionToLightSource3 = vec3(light3.position - vertexpos);
  vec3 positionToLightSource4 = vec3(light4.position - vertexpos);

  float distance1 = length(positionToLightSource1);
  float distance2 = length(positionToLightSource2);
  float distance3 = length(positionToLightSource3);
  float distance4 = length(positionToLightSource4);
  
  lightDirection1 = normalize(positionToLightSource1);
  lightDirection2 = normalize(positionToLightSource2);
  lightDirection3 = normalize(positionToLightSource3);
  lightDirection4 = normalize(positionToLightSource4);

  attenuation1 = 1.0 / (light1.constantAttenuation + light1.linearAttenuation * distance1 + light1.quadraticAttenuation * distance1 * distance1);    
  attenuation2 = 1.0 / (light2.constantAttenuation + light2.linearAttenuation * distance2 + light2.quadraticAttenuation * distance2 * distance2);    
  attenuation3 = 1.0 / (light3.constantAttenuation + light3.linearAttenuation * distance3 + light3.quadraticAttenuation * distance3 * distance3);    
  attenuation4 = 1.0 / (light4.constantAttenuation + light4.linearAttenuation * distance4 + light4.quadraticAttenuation * distance4 * distance4);    
    
  vec3 ambientLighting = vec3(scene_ambient) * vec3(frontMaterial.ambient);
  
  vec3 diffuseReflection1 = attenuation1 * vec3(light1.diffuse) * vec3(frontMaterial.diffuse) * max(0.0, dot(normalDirection, lightDirection1));
  vec3 diffuseReflection2 = attenuation2 * vec3(light2.diffuse) * vec3(frontMaterial.diffuse) * max(0.0, dot(normalDirection, lightDirection2));
  vec3 diffuseReflection3 = attenuation3 * vec3(light3.diffuse) * vec3(frontMaterial.diffuse) * max(0.0, dot(normalDirection, lightDirection3));
  vec3 diffuseReflection4 = attenuation4 * vec3(light4.diffuse) * vec3(frontMaterial.diffuse) * max(0.0, dot(normalDirection, lightDirection4));

  vec3 specularReflection1 = vec3(0.0, 0.0, 0.0);
  vec3 specularReflection2 = vec3(0.0, 0.0, 0.0);
  vec3 specularReflection3 = vec3(0.0, 0.0, 0.0);
  vec3 specularReflection4 = vec3(0.0, 0.0, 0.0);

  // check if the light is on the right side of the object
    if (!(dot(normalDirection, lightDirection1) < 0.0))
    {
      specularReflection1 = attenuation1 * vec3(light1.specularity) * vec3(frontMaterial.specularity) * pow(max(0.0, dot(reflect(-lightDirection1, normalDirection), viewDirection)), frontMaterial.shininess);
    }
	if (!(dot(normalDirection, lightDirection2) < 0.0))
    {
      specularReflection2 = attenuation2 * vec3(light2.specularity) * vec3(frontMaterial.specularity) * pow(max(0.0, dot(reflect(-lightDirection2, normalDirection), viewDirection)), frontMaterial.shininess);
    }
	if (!(dot(normalDirection, lightDirection3) < 0.0))
    {
      specularReflection3 = attenuation3 * vec3(light3.specularity) * vec3(frontMaterial.specularity) * pow(max(0.0, dot(reflect(-lightDirection3, normalDirection), viewDirection)), frontMaterial.shininess);
    }
	if (!(dot(normalDirection, lightDirection4) < 0.0))
    {
      specularReflection4 = attenuation4 * vec3(light4.specularity) * vec3(frontMaterial.specularity) * pow(max(0.0, dot(reflect(-lightDirection4, normalDirection), viewDirection)), frontMaterial.shininess);
    }
  
  vec3 diffuseReflection = (diffuseReflection1 + diffuseReflection2 + diffuseReflection3 + diffuseReflection4);
  vec3 specularReflection = (specularReflection1 + specularReflection2 + specularReflection3 + specularReflection4);

  outputColor = (vec4(ambientLighting + diffuseReflection + specularReflection, 1.0) * texture( pixels, uv ));
}

