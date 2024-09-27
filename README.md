
# README  
Hey! Thanks for giving me the assignment, I had a good time working on it. It took more time than I originally expected, since it's new Unity libraries that I have worked with few times in the past, and also since I go to the University at Thursdays and Fridays.  Nevertheless, it was a neat little project to work on. 

I decided to go a bit above the requirements for it and I hope you'll like it.  Rather than making a MonoBehaviour component that lets the user inspect textures, I made a Drawer Attribute that can be  added above any Texture2D variable in-code that will add the button that opens the new **Texture Inspector**.  
  
To enable the **Inspect** button, simply add the `[InspectTexture]` attribute.  
Works with both public and private using `[SerializeField]` variables.  

> [InspectTexture] public Texture2D publicTexture;  [InspectTexture] <br>
> [SerializeField] private Texture2D privateTexture;
<hr>

Package: `ExportedAssignmentPackage.unitypackage` <br>
Unity Version: `Unity 2019.4.40f1 Education` <br>
Using: `System, UnityEditor & UnityEngine`
