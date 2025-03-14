using UnityEngine;

namespace PLAYERTWO.PlatformerProject
{
	public interface IEntityContact
	{
		public void OnEntityContact(Entity entity);
	}
	
	public interface IEntityContactNormal
	{
		public void OnEntityContact(Entity entity, Vector3 contactNormal);
	}
	
}
