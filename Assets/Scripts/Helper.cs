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
   
   public static Bounds CalculateBounds (GameObject go)
   {
      Bounds bounds = new Bounds();
      bounds.size = Vector3.zero;
      Collider2D[] colliders = go.GetComponentsInChildren<Collider2D>();
      foreach (Collider2D col in colliders)
      {
         bounds.Encapsulate(col.bounds);
      }

      return bounds;
   }

   public static T RandomFromList<T>(List<T> list)
   {
      return list[Random.Range(0, list.Count - 1)];
   }
   
}
