using Unity.Entities;
using Unity.Mathematics;

public struct PlayerComponent:IComponentData
	{
		
	}
public struct InputComponent : IComponentData
	{
		
	}
public struct VelocityComponent : IComponentData
	{
		public float3 moveDir;   
	}
	

