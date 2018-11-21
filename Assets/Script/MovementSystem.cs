using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
//通过继承ComponentSystem来管理所有的entity 的Update
public class MovementSystem : ComponentSystem {
    
    struct myInput
    {
        /*
       [Inject]会从所有Entity中寻找同时拥有PlayerComponent+vVelocityComponent+InputComponent组件的实体，接着获取他们的这些组件，注入我们声明的不同数组中。
       我们只需要在结构中声明好筛选的条件与我们需要的组件，ECS就会在背后帮我们处理，给我们想要的结果。
        */
        public readonly int Length;
        public ComponentDataArray<PlayerComponent> players;
        public ComponentDataArray<VelocityComponent> velocities;
        public ComponentDataArray<InputComponent> inputs;
    }
    
    //sphere
    public struct SphereGroup
    {
        /*
        [Inject]会从所有Entity中寻找同时拥有VelocityComponent与Position组件的实体，接着获取他们的这些组件，注入我们声明的不同数组中。
        我们只需要在结构中声明好筛选的条件与我们需要的组件，ECS就会在背后帮我们处理，给我们想要的结果。
         */
        public readonly int Length;
        public ComponentDataArray<VelocityComponent> Velocities;
        public ComponentDataArray<Position> Positions;
    }
    //cube
    public struct CubeGameObject
    {
        /*
        [Inject]会从所有Entity中寻找同时拥有VelocityComponent与Position组件的实体，接着获取他们的这些组件，注入我们声明的不同数组中。
        我们只需要在结构中声明好筛选的条件与我们需要的组件，ECS就会在背后帮我们处理，给我们想要的结果。
         */
        public readonly int Length;
        public ComponentArray<Transform> Transforms; //该数组可以获取传统的Component
        public ComponentDataArray<VelocityComponent> Velocities;//该数组获取继承IComponentData的
    }
    //声明结构类型的字段, 并且加上[Inject]
    [Inject] private SphereGroup data;
    
    [Inject] private CubeGameObject go;

    [Inject] private myInput _input;
    protected override void OnUpdate()
    {
        
        for (int i = 0; i < _input.Length; i++)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");
            float3 normalized=new float3();
            if (x!=0||z!=0)
            {
                normalized = math.normalize(new float3(x, 0, z));
            } 
            _input.velocities[i]=new VelocityComponent{moveDir = normalized};
        }
        //sphere
        float deltaTime = Time.deltaTime;
        for (int i = 0; i < data.Length; i++)
        {
            float3 pos=data.Positions[i].Value;
            float3 vector = data.Velocities[i].moveDir;
            pos += vector * deltaTime; //Move
            data.Positions[i]=new Position{Value = pos};
        }
        //cube
        for (int i = 0; i < go.Length; i++)
        {
            float3 pos = go.Transforms[i].position;
            float3 vector = go.Velocities[i].moveDir;
            pos += vector*deltaTime;
            go.Transforms[i].position=pos;
        }
    }
}
