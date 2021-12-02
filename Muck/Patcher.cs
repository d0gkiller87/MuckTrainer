using HarmonyLib;

namespace bruh {
    static class Patcher {
        public static Harmony harmony;
        public static void Init() { 
            harmony = new Harmony("bruh.muck");
            harmony.PatchAll();
        }
    }

    [HarmonyPatch(typeof(PlayerStatus))]
    class PatchedPlayerStatus {
        [HarmonyPrefix]
        [HarmonyPatch("HandleDamage")]
        static bool HandleDamage() {
            if (Config.config["GodMode"].isEnabled) {
                DamageVignette.Instance.VignetteHit();
                return false;
            } else {
                return true;
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch("Hunger")]
        static bool Hunger() {
            if (Config.config["NoHunger"].isEnabled) {
                return false;
            } else {
                return true;
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch("Stamina")]
        static bool Stamina(ref PlayerStatus __instance, ref float ___staminaRegenRate) {
            if (Config.config["InfiniteStamina"].isEnabled) {
                if (__instance.stamina < 100f) {
                   __instance.stamina += ___staminaRegenRate * UnityEngine.Time.deltaTime;
                }
                return false;
            } else { 
                return true;
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch("Jump")]
        static bool Jump() {
            return !Config.config["InfiniteStamina"].isEnabled;
        }
    }

    [HarmonyPatch(typeof(PlayerMovement))]
    class PatchedPlayerMovement {
        [HarmonyPrefix]
        [HarmonyPatch("CheckInput")]
        static bool CheckInput(ref PlayerMovement __instance, ref PlayerStatus ___playerStatus, ref float ___maxSpeed, ref float ___maxWalkSpeed) {
            if (Config.config["SpeedHack"].isEnabled) {
                if (__instance.crouching && !__instance.sliding) {
                    __instance.StartCrouch();
                }
                if (!__instance.crouching && __instance.sliding) {
                    __instance.StopCrouch();
                }
                if (__instance.sprinting && ___playerStatus.CanRun()) {
                    ___maxSpeed = 50f;
                    return false;
                }
                ___maxSpeed = ___maxWalkSpeed;
                return false;
            } else { 
                return true;    
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch("Jump")]
        static void Jump(ref int ___jumps) {
            if (Config.config["JumpHack"].isEnabled) { 
                ___jumps = 1;
            }
        }
    }

    [HarmonyPatch(typeof(HitableActor))]
    class PatchedHitableActor {
        [HarmonyPrefix]
        [HarmonyPatch("Hit")]
        static void Hit(ref int damage) {
            if (Config.config["OneHitPlayers"].isEnabled) {
                damage += Config.Damage;
            }
        }
    }

    [HarmonyPatch(typeof(HitableMob))]
    class PatchedHitableMob {
        [HarmonyPrefix]
        [HarmonyPatch("Hit")]
        static bool Hit(ref int ___id, ref int damage, ref float sharpness, ref int hitEffect, ref UnityEngine.Vector3 hitPos, ref int hitWeaponType) {
            if (Config.config["OneHitMobs"].isEnabled) {
                ClientSend.PlayerDamageMob(___id, damage + Config.Damage, sharpness, hitEffect, hitPos, hitWeaponType);
                return false;
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(HitableResource))]
    class PatchedHitableResource {
        [HarmonyPrefix]
        [HarmonyPatch("Hit")]
        static void Hit(ref int damage) {
            if (Config.config["OneHitResource"].isEnabled) {
                if (Config.config["IgnoreToolLevel"].isEnabled) {
                    damage += Config.Damage;
                } else { 
                    damage *= Config.DamageMutiplier;
                }
            } else {
                if (Config.config["IgnoreToolLevel"].isEnabled) {
                    damage += Config.DamageNormal;
                }
            }
        }
    }

    [HarmonyPatch(typeof(LootContainerInteract))]
    class PatchedLootContainerInteract {
        [HarmonyPrefix]
        [HarmonyPatch("Interact")]
        static bool Interact(ref int ___id) {
            if (Config.config["ForceUnlockBoxes"].isEnabled) {
		        ClientSend.PickupInteract(___id);
                return false;
            } else { 
                return true; 
            }
	    }
    }

    [HarmonyPatch(typeof(RepairInteract))]
    class PatchedRepairInteract {
        [HarmonyPrefix]
        [HarmonyPatch("Interact")]
        static bool Interact(ref int ___id) {
            if (Config.config["ForceRepairBoatComponents"].isEnabled) {
		        ClientSend.Interact(___id);
                return false;
            } else { 
                return true;
            }
	    }
    }

    [HarmonyPatch(typeof(Boat))]
    class PatchedBoat {
        [HarmonyPrefix]
        [HarmonyPatch("CheckBoatFullyRepaired")]
        static bool CheckBoatFullyRepaired(ref bool __result) {
            if (Config.config["BoatAlreadyRepaired"].isEnabled) {
                __result = true;
                return false;
            } else { 
                return true;
            }
	    }
    }
}
