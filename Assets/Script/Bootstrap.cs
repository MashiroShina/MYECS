using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

public class Bootstrap
{
	private static EntityManager entityManager;//所有实体的管理器, 提供操作Entity的API
	private static EntityArchetype playerArchetype;//Entity原型, 可以看成由组件组成的数组
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	public static void Awake()
	{
		entityManager = World.Active.GetOrCreateManager<EntityManager>();
		//下面的的Position类型需要引入Unity.Transforms命名空间
		playerArchetype = entityManager.CreateArchetype(typeof(Position));
	}
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
	public static void start() {
		//把GameObect.Find放在这里因为场景加载完成前无法获取游戏物体。
		GameObject playerGo=GameObject.Find("Player");
		GameObjectEntity cubeEntity = GameObject.Find("Cube").AddComponent<GameObjectEntity>();
		
		//下面的类型是一个Struct, 需要引入Unity.Rendering命名空间
		MeshInstanceRenderer playerRenderer = playerGo.GetComponent<MeshInstanceRendererComponent>().Value;
		
		//获取到渲染数据后可以销毁空物体
		Object.Destroy(playerGo);

		Entity player = entityManager.CreateEntity(playerArchetype);
		
		//sphere
		entityManager.AddComponentData(player,new PlayerComponent());
		entityManager.AddComponentData(player,new VelocityComponent());
		entityManager.AddComponentData(player,new InputComponent());
		//cube 这里如果给cube添加跟sphere相同的Component那么在ComponentSystem中可以相同的控制
		entityManager.AddComponentData(cubeEntity.Entity,new VelocityComponent{moveDir = new float3(0,1,0)});
//		entityManager.AddComponentData(cubeEntity.Entity,new PlayerComponent());
//		entityManager.AddComponentData(cubeEntity.Entity,new InputComponent());		
		
		entityManager.SetComponentData(player,new Position{Value = new float3(0,0.5f,0)});
		// 向实体添加共享数据组件
		entityManager.AddSharedComponentData(player,playerRenderer);
		
	}

}

