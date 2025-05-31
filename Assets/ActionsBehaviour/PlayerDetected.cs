using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/PlayerDetected")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "PlayerDetected", message: "[Enemy] has spotted [Player]", category: "Events", id: "916032df6ff02821dacb1661f499c399")]
public sealed partial class PlayerDetected : EventChannel<GameObject, GameObject> { }

