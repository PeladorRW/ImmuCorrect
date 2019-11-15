using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace ImmuCorrect
{
    public class ImmuCorrection
    {
        public static void correct_missing_immu()
        {
            if (Controller.Settings.AllowReset)
            {
                int count = 0;
                List<Pawn> pawns = PawnsFinder.All_AliveOrDead.ToList<Pawn>();
                if (pawns.Count > 0)
                {
                    foreach (Pawn pawn in pawns)
                    {
                        List<ImmunityRecord> newIRList = new List<ImmunityRecord>();
                        if (pawn?.health?.immunity != null)
                        {
                            List<ImmunityRecord> IList = NonPublicFields.ImmunityList(pawn.health.immunity);
                            if (IList.Count > 0)
                            {
                                foreach(ImmunityRecord IR in IList)
                                {
                                    HediffDef def = IR?.hediffDef;
                                    if (def != null)
                                    {
                                        if (DefDatabase<HediffDef>.GetNamed(def?.defName, false) != null)
                                        {
                                            newIRList.Add(IR);
                                        }
                                    }
                                }
                            }
                            
                            count++;
                            NonPublicFields.ImmunityList(pawn.health.immunity) = newIRList;
                        }
                    }
                }
                Messages.Message("ImmuCorrect.Message".Translate(count.ToString()), MessageTypeDefOf.CautionInput, false);
            }
            else
            {
                Messages.Message("ImmuCorrect.NotAllow".Translate(), MessageTypeDefOf.RejectInput, false);
            }
        }
    }
}
