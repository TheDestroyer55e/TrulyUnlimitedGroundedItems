using MonoMod.Cil;
using System;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ModLoader;


namespace UnlimitedGroundedItems
{
	public class UnlimitedGroundedItems : Mod
	{
		public override void Load() {

			int itemsCap = ContentInstance<Config>.Instance.groundedItemsCap;

			Main.item = new Item[itemsCap+1];
            Main.timeItemSlotCannotBeReusedFor = new int[itemsCap+1];
			Main.itemFrame = new int[itemsCap + 1];
			Main.itemFrameCounter = new int[itemsCap+1];


            // Main hooks
            IL_Main.Initialize_Entities += il => ReplaceItemsCap(il, 401, itemsCap+1);
            
			IL_Main.DoUpdateInWorld += il => ReplaceItemsCap(il, 400, itemsCap);
            
            IL_Main.DrawItems += il => ReplaceItemsCap(il, 400, itemsCap);
            
            IL_Main.DrawMouseOver += il => ReplaceItemsCap(il, 400, itemsCap);
            
            IL_Main.UpdateClient += il => ReplaceItemsCap(il, 400, itemsCap);

            IL_Main.UpdateServer += il => {
                ILCursor c = new ILCursor(il);
                c.GotoNext(i => i.MatchLdcI4(400));
                c.Next.Operand = itemsCap;
                c.GotoNext(i => i.MatchLdcI4(400));
                c.Next.Operand = itemsCap;
            };


            // Item hooks
            IL_Item.MechSpawn += il => ReplaceItemsCap(il, 200, itemsCap/2);

            IL_Item.MoveInWorld += il => ReplaceItemsCap(il, 400, itemsCap);

            IL_Item.CombineWithNearbyItems += il => ReplaceItemsCap(il, 400, itemsCap);

            IL_Item.NewItem_Inner += il => {
                ILCursor c = new ILCursor(il);
                c.GotoNext(i => i.MatchLdcI4(400));
                c.Next.Operand = itemsCap;
                c.GotoNext(i => i.MatchLdcI4(400));
                c.Next.Operand = itemsCap;
                c.GotoNext(i => i.MatchLdcI4(400));
                c.Next.Operand = itemsCap;
            };

            IL_Item.PickAnItemSlotToSpawnItemOn += il => {
                ILCursor c = new ILCursor(il);
                c.GotoNext(i => i.MatchLdcI4(400));
                c.Next.Operand = itemsCap;
                c.GotoNext(i => i.MatchLdcI4(399));
                c.Next.Operand = itemsCap-1;
                c.GotoNext(i => i.MatchLdcI4(400));
                c.Next.Operand = itemsCap;
                c.GotoNext(i => i.MatchLdcI4(400));
                c.Next.Operand = itemsCap;
            };


            // MessageBuffer hook
            IL_MessageBuffer.GetData += il => {
                ILCursor c = new ILCursor(il);
                c.GotoNext(i => i.MatchLdcI4(400));
                c.Next.Operand = itemsCap;
                c.GotoNext(i => i.MatchLdcI4(400));
                c.Next.Operand = itemsCap;
                c.GotoNext(i => i.MatchLdcI4(400));
                c.Next.Operand = itemsCap;
                c.GotoNext(i => i.MatchLdcI4(400));
                c.Next.Operand = itemsCap;
            };


            // Player hook
            IL_Player.GrabItems += il => ReplaceItemsCap(il, 400, itemsCap);


            // WorldGen hooks
            IL_WorldGen.clearWorld += il => ReplaceItemsCap(il, 400, itemsCap);

            IL_WorldGen.TryProtectingSpawnedItems += il => ReplaceItemsCap(il, 400, itemsCap);

            IL_WorldGen.UndoSpawnedItemProtection += il => ReplaceItemsCap(il, 400, itemsCap);


            // DD2Event hook
            IL_DD2Event.ClearAllDD2EnergyCrystalsInGame += il => ReplaceItemsCap(il, 400, itemsCap);


        }

        private void ReplaceItemsCap(ILContext il, int oldValue, int newValue) {
            ILCursor c = new ILCursor(il);
            c.GotoNext(i => i.MatchLdcI4(oldValue));
            c.Next.Operand = newValue;
        }
	}
}