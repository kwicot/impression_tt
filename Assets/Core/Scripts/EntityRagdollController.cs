using PLAYERTWO.PlatformerProject;
using RootMotion.Dynamics;
using UnityEngine;

[RequireComponent(typeof(Player))]
[AddComponentMenu("PLAYER TWO/Platformer Project/Entity/Entity Ragdoll Controller")]
public class EntityRagdollController : MonoBehaviour
{
    public PuppetMaster m_puppetMaster;
    protected Player m_player;
    
    protected virtual void InitializePlayer() => m_player = GetComponent<Player>();
    protected virtual void InitializeCallbacks()
    {
        m_player.playerEvents.OnHurt.AddListener(Activate);
        m_player.states.events.onChange.AddListener(OnStateChange);
    }

    private void OnStateChange()
    {
        if (m_player.states.IsCurrentOfType(typeof(IdlePlayerState)))
        {
            Deactivate();
        }
    }

    protected virtual void Start()
    {
        InitializePlayer();
        InitializeCallbacks();
    }

    void Activate()
    {
        m_puppetMaster.SwitchToActiveMode();
    }

    void Deactivate()
    {
        m_puppetMaster.SwitchToDisabledMode();        
    }

    void Update()
    {
        
    }
}
