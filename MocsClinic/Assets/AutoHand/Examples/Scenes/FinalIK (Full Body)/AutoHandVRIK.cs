using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

namespace Autohand {
    [DefaultExecutionOrder(5)]
    public class AutoHandVRIK : MonoBehaviour {
        public VRIK IK;
        public Hand rightHand;
        public Hand leftHand;
        [Tooltip("Should be a transform under the Auto Hand, can be used to adjust the IK offset so the hands connect with the arms properly")]
        public Transform rightIKTarget;
        [Tooltip("Should be a transform under the Auto Hand, can be used to adjust the IK offset so the hands connect with the arms properly")]
        public Transform leftIKTarget;
        [Tooltip("Should be a transform under the IK Character hierarchy, can be used to adjust the IK offset so the hands connect with the arms properly")]
        public Transform rightHandFollowTarget;
        [Tooltip("Should be a transform under the IK Character hierarchy, can be used to adjust the IK offset so the hands connect with the arms properly")]
        public Transform leftHandFollowTarget;
        [Tooltip("The transform (or a child transform) of the Tracked VR controller")]
        public Transform rightTrackedController;
        [Tooltip("The transform (or a child transform) of the Tracked VR controller")]
        public Transform leftTrackedController;

        VRIK visibleIK;
        bool isCopy = false;

        public void DesignateCopy() {
            isCopy = true;
        }

        void Start() {
            if(!isCopy)
                SetupIKCopy();

            if(AutoHandPlayer.Instance != null)
                IK.transform.position -= Vector3.up * AutoHandPlayer.Instance.heightOffset;
        }

        void SetupIKCopy() {
            visibleIK = Instantiate(IK.gameObject, IK.transform.parent).GetComponent<VRIK>();
            AutoHandVRIK copy = visibleIK.GetComponent<AutoHandVRIK>();
            if(copy == null) copy = visibleIK.GetComponentInChildren<AutoHandVRIK>();
            copy?.DesignateCopy();

            DeactivateEverything(IK.transform);
            IK.enabled = true;
            if(IK.CanGetComponent<AutoHandVRIK>(out var AutoIK2)) AutoIK2.enabled = true;
            IK.solver.rightArm.target = rightTrackedController;
            IK.solver.leftArm.target = leftTrackedController;

            visibleIK.solver.rightArm.target = rightIKTarget;
            visibleIK.solver.leftArm.target = leftIKTarget;
            rightHand.follow = rightHandFollowTarget;
            leftHand.follow = leftHandFollowTarget;
        }

        void DeactivateEverything(Transform deactivate) {
            var behaviours = deactivate.GetComponents<Component>();
            var childBehaviours = deactivate.GetComponentsInChildren<Component>();
            for(int j = behaviours.Length - 1; j >= 0; j--)
                if(!(behaviours[j] is Animator) && !(behaviours[j] is VRIK) && !(behaviours[j] is AutoHandVRIK) && !(behaviours[j] is Transform))
                    Destroy(behaviours[j]);
            for(int j = childBehaviours.Length - 1; j >= 0; j--)
                if(!(childBehaviours[j] is Animator) && !(childBehaviours[j] is VRIK) && !(childBehaviours[j] is AutoHandVRIK) && !(childBehaviours[j] is Transform))
                    Destroy(childBehaviours[j]);
        }
    }

}