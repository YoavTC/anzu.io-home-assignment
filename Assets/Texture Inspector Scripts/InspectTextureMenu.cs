using System;
using UnityEditor;
using UnityEngine;

public class InspectTextureMenu : EditorWindow
{
    //Window settings
    private static Rect windowRect;
    private const int windowSize = 500;
    
    //Controls
    private Vector2 pan;
    private Vector2 mousePosition;
    private float zoom = 1f;
    private readonly int zoomDirection = -1; //1 normal
                                             //-1 reversed

    //Cache
    private Rect previewRect;
    private Texture2D inspectedTexture2D;
    private bool repaint;
    private float oldZoom;
    
    private void OnEnable()
    {
        windowRect = new Rect(
            Screen.currentResolution.width / 2f,
            Screen.currentResolution.height / 2f,
            windowSize,
            windowSize
        );
        Repaint();
    }

    public static void OpenWindow(Texture2D texture2D)
    {
        //Close and reopen window to prevent bugs
        GetWindow<InspectTextureMenu>().Close();
        InspectTextureMenu window = GetWindowWithRect<InspectTextureMenu>(windowRect, focusedWindow, GetTitleTooltip(texture2D), true);

        window.inspectedTexture2D = texture2D;
    }

    private void OnGUI()
    {
        //Draw preview
        previewRect = new Rect(pan.x, pan.y, windowRect.width * zoom, windowRect.height * zoom);
        GUI.DrawTexture(previewRect, inspectedTexture2D, ScaleMode.ScaleToFit);
        
        HandleInput(Event.current);
    }

    private void HandleInput(Event currentEvent)
    {
        //Repaint bool in order to use Repaint(); ONLY when necessary
        repaint = false;
        
        //Zoom view
        if (currentEvent.type == EventType.ScrollWheel)
        {   repaint = true;
            mousePosition = currentEvent.mousePosition;
            pan = mousePosition + (pan - mousePosition) * GetZoomFactor(currentEvent);
        }

        //Pan view
        if (currentEvent.type == EventType.MouseDrag && currentEvent.button == 0)
        {   repaint = true;
            Vector2 panDelta = pan + currentEvent.delta;
            
            //Window constraints
            if (IsRectSmaller(previewRect, windowRect))
            {
                panDelta.x = Mathf.Clamp(panDelta.x, 0, windowRect.width - previewRect.width);
                panDelta.y = Mathf.Clamp(panDelta.y, 0, windowRect.height - previewRect.height);
            }
            pan = panDelta;
        }

        //Reset view
        if (currentEvent.type == EventType.KeyDown && currentEvent.keyCode == KeyCode.R)
        {   repaint = true;
            zoom = 1f;
            pan = Vector2.zero;
        }
        
        if (repaint) Repaint();
    }

    #region Utility
    private float GetZoomFactor(Event currentEvent)
    {
        oldZoom = zoom;
        zoom = Mathf.Clamp(zoom + (zoomDirection * (currentEvent.delta.y * 0.01f)), 0.1f, 5f);
        return zoom / oldZoom;
    }

    private bool IsRectSmaller(Rect a, Rect b) => a.width < b.width && a.height < b.height;
    
    private static string GetTitleTooltip(Texture2D texture2D)
    {
        return string.Format("Texture Inspector Menu | {0} ({1}x{2}) | Press R to Reset View",
            texture2D.name, texture2D.width, texture2D.height);
    }
    
    #endregion
}