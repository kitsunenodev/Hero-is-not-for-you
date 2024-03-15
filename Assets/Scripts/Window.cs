using UnityEngine;
public class Window : MonoBehaviour
{

   public Sprite close;
   
   public Sprite open;
   
   public SpriteRenderer sprRenderer;

   public void ChangeSprite()
   {
      if (sprRenderer.sprite == open)
      {
         sprRenderer.sprite = close;
      }
      else
      {
         sprRenderer.sprite = open;

      }
   } 
}
