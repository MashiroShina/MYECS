//using UnityEngine;
//using Unity.Entities;
//using Unity.Mathematics;
//
//public class InputSystem : ComponentSystem
//{
//    struct Group
//    {
//        public readonly int Length;
//        public ComponentDataArray<PlayerComponent> players;
//        public ComponentDataArray<VelocityComponent> velocities;
//        public ComponentDataArray<InputComponent> inputs;
//    }
//
//    [Inject] private Group data;
//    protected override void OnUpdate()
//    {
//        for (int i = 0; i < data.Length; i++)
//        {
//            float x = Input.GetAxisRaw("Horizontal");
//            float z = Input.GetAxisRaw("Vertical");
//            float3 normalized=new float3();
//            if (x!=0||z!=0)
//            {
//                normalized = math.normalize(new float3(x, 0, z));
//            }
//            data.velocities[i]=new VelocityComponent{moveDir = normalized};
//        }
//    }
//}