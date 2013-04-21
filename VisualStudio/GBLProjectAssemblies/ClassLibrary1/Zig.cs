using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GBLProjectAssemblies;

public class Zig : MonoBehaviour
{
    public ZigInputType inputType = ZigInputType.OpenNI2;
    //public bool UpdateDepthmap = true;
    //public bool UpdateImagemap = false;
    //public bool UpdateLabelmap = false;
    //public bool AlignDepthToRGB = false;
    public ZigInputSettings settings = new ZigInputSettings();
    public bool Verbose = true;

    public GameObject InputListenerObject;

    void Awake()
    {
#if UNITY_WEBPLAYER
#if UNITY_EDITOR
        Debug.LogError("Depth camera input will not work in editor when target platform is Webplayer. Please change target platform to PC/Mac standalone.");
        return;
#endif
#endif

        ZigInput.InputType = inputType;
        ZigInput.Settings = settings;
        //ZigInput.UpdateDepth = UpdateDepthmap;
        //ZigInput.UpdateImage = UpdateImagemap;
        //ZigInput.UpdateLabelMap = UpdateLabelmap;
        //ZigInput.AlignDepthToRGB = AlignDepthToRGB;
        ZigInput.Instance.AddListener(gameObject);
    }



    void Zig_UserLost(ZigTrackedUser user)
    {
        if (Verbose) Debug.Log("Zig: Lost user " + user.Id);
    }

    void Zig_Update(ZigInput zig)
    {

    }

    void Zig_UpdateUser(ZigTrackedUser user)
    {
        if (user.SkeletonTracked)
        {
            foreach (ZigInputJoint joint in user.Skeleton)
            {
                if (joint.Id == ZigJointId.Waist)
                {
                    var pos = joint.Position;
                    GameController.Instance.UserRelativePosition = new Vector3(pos.z, pos.y, pos.x);
                }
            }
        }
        //Debug.Log("Tracked: " + tracked + " untracked: " + untracked);
    }

    void Zig_UserFound(ZigTrackedUser user)
    {
        user.AddListener(this.gameObject);
        Debug.Log("Zig: Found user YO  " + user.Id);
    }

}
