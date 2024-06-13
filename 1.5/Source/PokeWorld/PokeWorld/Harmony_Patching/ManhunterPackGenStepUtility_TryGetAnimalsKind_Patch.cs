﻿using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;

namespace PokeWorld
{
    [HarmonyPatch(typeof(ManhunterPackGenStepUtility))]
    [HarmonyPatch(nameof(ManhunterPackGenStepUtility.TryGetAnimalsKind))]
    class ManhunterPackGenStepUtility_TryGetAnimalsKind_Patch
    {
        public static bool Prefix(float __0, int __1, out PawnKindDef __2, ref bool __result)
        {
            if (PokeWorldSettings.OkforPokemon())
            {
                List<PawnKindDef> list = new List<PawnKindDef>();
                for (int i = 0; i < 50; i++)
                {
                    if (!Pokemon_ManhunterPackIncidentUtility.TryFindManhunterAnimalKind(__0, __1, out list))
                    {
                        continue;
                    }
                    break;
                }
                if (list.Count <= 0)
                {
                    for (int j = 0; j < 50; j++)
                    {
                        if (!Pokemon_ManhunterPackIncidentUtility.TryFindManhunterAnimalKind(__0, -1, out list))
                        {
                            continue;
                        }
                        break;
                    }
                }
                if (list.Count <= 0)
                {
                    __2 = null;
                    __result = false;
                }
                else
                {
                    __2 = list.RandomElement();
                    __result = true;
                }
            }
            else
            {
                PawnKindDef kind = null;
                for (int i = 0; i < 50; i++)
                {
                    if (!AggressiveAnimalIncidentUtility.TryFindAggressiveAnimalKind(__0, __1, out kind))
                    {
                        continue;
                    }
                    break;
                }
                if (kind == null)
                {
                    for (int j = 0; j < 50; j++)
                    {
                        if (!AggressiveAnimalIncidentUtility.TryFindAggressiveAnimalKind(__0, -1, out kind))
                        {
                            continue;
                        }
                        break;
                    }
                }
                if (kind == null)
                {
                    __2 = null;
                    __result = false;
                }
                else
                {
                    __2 = kind;
                    __result = true;
                }
            }
            return false;
        }
    }
}
