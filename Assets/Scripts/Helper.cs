using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
   private static Camera camera;
   public static Camera Camera
   {
      get
      {
         if(camera == null) camera = Camera.main;
         return camera;
      }
   }
}
