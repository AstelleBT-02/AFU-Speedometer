using MelonLoader;
using HarmonyLib;
using UnityEngine;
using Il2CppPhoton.Deterministic;
using Il2CppQuantum.Core;
using Il2CppQuantum;
using Il2CppSystem.Threading.Tasks;
using UnityEngine.UIElements;

[assembly: MelonInfo(typeof(Speedometer.Core), "Speedometer", "1.0.0", "taldo", null)]
[assembly: MelonGame("Videocult", "Airframe")]

namespace Speedometer
{
    public class Core : MelonMod
    {
        internal static MelonLogger.Instance Log => Melon<Core>.Instance.LoggerInstance;
        internal static Speedometer.Core Inst => Melon<Core>.Instance;
        
        static FPVector3 lastPos;
        static int true_speed_upper;
        static int true_speed_lower;
        static bool on_bike;
        [HarmonyPatch(typeof(FrameContext), "OnFrameSimulationBegin")]
        private partial class Simulate
        {
            public static void Postfix(FrameBase f)
            {     
                Frame ff = f.Cast<Frame>();
                Il2CppSystem.Collections.Generic.List<EntityRef> all_Erefs = new();
                f.GetAllEntityRefs(all_Erefs);
                
                foreach (EntityRef eref in all_Erefs)
                {
                    if (f.Has<HoverBike>(eref)) 
                {
                    // get position
                    Transform3D hb_phys = f.Get<Transform3D>(eref);
                    FPVector3 currPos = hb_phys.Position;
                    // calculate velociy
                    FPVector3 vel = currPos - lastPos;
                    // convert to usable format
                    vel.Y = FP._0;
                    FP speed = vel.Magnitude * 45 * 100; // centi-units per tick (cu/t)
                    //double speed_d = Math.Truncate((double)speed * 10) / 10;
                    int speed_i = (int)speed;

                    // report speed to be displayed
                    true_speed_upper = speed_i / 10;
                    true_speed_lower = speed_i % 10;
                    //Log.Msg(speed_i);
                    lastPos = currPos;
                } 
                    if (f.Has<Player>(eref))
                {
                    Player player = f.Get<Player>(eref);
                    if (f.Context.IsLocalPlayer(player.playerRef)) {
                    if (f.Exists(player.controlledEntity))
                    {
                        Humanoid human = f.Get<Humanoid>(player.controlledEntity);
                        on_bike = f.Exists(human.vehicle);
                    }
                    
                } } }
            }
        }

        public override void OnGUI()
        {
            if (on_bike) {
            base.OnGUI();
            Resolution res = Screen.currentResolution;
                
            // BORDER
            GUI.Box(new Rect
            (
                res.width / 2 - 70,
                res.height / 2 * 1.15F,
                150, 
                70   
            ), 
                new Texture()
            );
            
            // UPPER DIGITS
            GUI.Box(new Rect
            (
                res.width / 2 - 20, 
                res.height / 2 * 1.2F,
                60, 
                20
            ), 
                $"{true_speed_upper}",

                new GUIStyle()
                {
                    fontSize = 60,
                    alignment = TextAnchor.MiddleRight,
                    normal = new GUIStyleState(){textColor = Color.white}
                }
            );

            // LOWER DIGIT
            GUI.Box(new Rect
            (
                res.width / 2 + 40, 
                res.height / 2 * 1.211F,
                60, 
                20 
            ), 
                $"{true_speed_lower}",

                new GUIStyle()
                {
                    fontSize = 40,
                    alignment = TextAnchor.MiddleLeft,
                    normal = new GUIStyleState(){textColor = Color.white}
                }
            );

        } }
    }
}