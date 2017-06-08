using UnityEngine;

public static class Helper
{
    public struct ClipPlanePoints
    {
        public Vector3 UpperLeft;
        public Vector3 UpperRight;
        public Vector3 LowerRight;
        public Vector3 LowerLeft;
    }
    public static float ClampAngle(float angle, float min, float max)
    {
        do
        {
            if (angle < -360)
                angle += 360;

            if (angle > 360)
                angle -= 360;

        } while (angle < -360 || angle > 360);

        return Mathf.Clamp(angle, min, max);
    }

    
    public static ClipPlanePoints ClipPlaneAtNear(Vector3 pos)
    {
        var clipPlanePoints = new ClipPlanePoints();

        if (Camera.main == null)
            return clipPlanePoints;

        var transform = Camera.main.transform;
        var halfFOV = (Camera.main.fieldOfView / 2) * Mathf.Deg2Rad;
        var aspect = Camera.main.aspect;
        var distance = Camera.main.nearClipPlane;
        var height = distance * Mathf.Tan(halfFOV);
        var width = height * aspect;

        //LOWER RIGHT
        //Move our point to right from position by width
        clipPlanePoints.LowerRight = pos + transform.right * width;

        //Move our point down i.e. down by height
        clipPlanePoints.LowerRight -= transform.up * height;

        //Ensures that we are moving forward relative to camera
        clipPlanePoints.LowerRight += transform.forward * distance;

        //LOWER LEFT
        //Move our point to left from position by width
        clipPlanePoints.LowerLeft = pos - transform.right * width;

        //Move our point down i.e. down by height
        clipPlanePoints.LowerLeft -= transform.up * height;

        //Ensures that we are moving forward relative to camera
        clipPlanePoints.LowerLeft += transform.forward * distance;

        //UPPER RIGHT
        //Move our point to right from position by width
        clipPlanePoints.UpperRight = pos + transform.right * width;

        //Move our point up i.e. up by height
        clipPlanePoints.UpperRight += transform.up * height;

        //Ensures that we are moving forward relative to camera
        clipPlanePoints.UpperRight += transform.forward * distance;

        //UPPER LEFT
        //Move our point to left from position by width
        clipPlanePoints.UpperLeft = pos - transform.right * width;

        //Move our point up i.e. up by height
        clipPlanePoints.UpperLeft += transform.up * height;

        //Ensures that we are moving forward relative to camera
        clipPlanePoints.UpperLeft += transform.forward * distance;


        return clipPlanePoints;
    }
}