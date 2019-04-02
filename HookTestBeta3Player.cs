using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace HookTestBeta3
{
    public class HookTestBeta3Player : ModPlayer
    {
        DateTime TimeHealed { get; set; }

        public override bool ModifyNurseHeal(NPC nurse, ref int health, ref bool removeDebuffs, ref string chatText)
        {
            if (TimeHealed == null)
                TimeHealed = DateTime.Now;

            Main.NewText($"--------------------------------- ModifyNurseHeal");
            Main.NewText($"               Speaking to Nurse: {nurse.FullName}, {nurse.GivenName}");
            Main.NewText($"              Health Before Edit: {health}");
            Main.NewText($"  RemoveDebuffs Flag Before Edit: {removeDebuffs}");
            Main.NewText($"            ChatText before Edit: {chatText}");
            Main.NewText($"---------------------------------");

            health = 2;
            chatText = $"Hello. I would like to heal you for {health} health. You're keeping your debuffs though because I'm mean.";
            removeDebuffs = false;

            if(TimeHealed.AddSeconds(20) > DateTime.Now)
            {
                TimeSpan? variable = DateTime.Now - TimeHealed;
                chatText = $"Please wait {20 - variable.Value.Seconds} more seconds to heal.";
                Main.NewText($"Time left before you can heal again: {20 - variable.Value.Seconds}");
                return false;
            }
            else
            {
                TimeHealed = DateTime.Now;
                return true;
            }
        }

        public override void ModifyNursePrice(NPC nurse, int health, bool removeDebuffs, ref int price)
        {
            Main.NewText($"--------------------------------- ModifyNursePrice");
            Main.NewText($"    Healed by Nurse: {nurse.FullName}, {nurse.GivenName}");
            Main.NewText($"      Health Healed: {health}");
            Main.NewText($"     RemoveDebuffs?: {removeDebuffs}");
            Main.NewText($"  Price before Edit: {price}");
            Main.NewText($"---------------------------------");

            price = 1;
        }

        public override void PostNurseHeal(NPC nurse, int health, bool removeDebuffs, int price)
        {
            Main.NewText($"--------------------------------- PostNurseHeal");
            Main.NewText($"  Healed by Nurse: {nurse.FullName}, {nurse.GivenName}");
            Main.NewText($"    Health Healed: {health}");
            Main.NewText($"   RemoveDebuffs?: {removeDebuffs}");
            Main.NewText($"    Price of Heal: {price}");
            Main.NewText($"---------------------------------");
        }

        public override void PostSellItem(NPC vendor, Item[] shopInventory, Item item)
        {
            Main.NewText($"--------------------------------- PostNurseHeal");
            Main.NewText($"                       Item sold to: {vendor.FullName}, {vendor.GivenName}");
            Main.NewText($"  Current items in shop before sale: {string.Join(",", shopInventory.Select(x => x.Name))}");
            Main.NewText($"         Item that player just sold: {item.Name}");
            Main.NewText($"---------------------------------");
        }

        public override bool CanSellItem(NPC vendor, Item[] shopInventory, Item item)
        {
            Main.NewText($"--------------------------------- CanSellItem");
            Main.NewText($"  NPC that player is trying to sell to: {vendor.FullName}, {vendor.GivenName}");
            Main.NewText($"     Current items in shop before sale: {string.Join(",", shopInventory.Select(x => x.Name))}");
            Main.NewText($"         Item player is trying to sell: {item.Name}");
            Main.NewText($"---------------------------------");

            if (item.Name == "Shuriken")
                return false;

            return true;
        }

        public override void PostBuyItem(NPC vendor, Item[] shopInventory, Item item)
        {
            Main.NewText($"--------------------------------- PostBuyItem");
            Main.NewText($"  NPC that player bought from: {vendor.FullName}, {vendor.GivenName}");
            Main.NewText($"        Current items in shop: {string.Join(",", shopInventory.Select(x => x.Name))}");
            Main.NewText($"      Item that player bought: {item.Name}");
            Main.NewText($"---------------------------------");
        }

        public override bool CanBuyItem(NPC vendor, Item[] shopInventory, Item item)
        {
            Main.NewText($"--------------------------------- CanSellItem");
            Main.NewText($"  NPC that player is trying to buy from: {vendor.FullName}, {vendor.GivenName}");
            Main.NewText($"                  Current items in shop: {string.Join(",", shopInventory.Select(x => x.Name))}");
            Main.NewText($"           Item player is trying to buy: {item.Name}");
            Main.NewText($"---------------------------------");

            if (item.Name == "Torch")
                return false;

            return true;
        }
    }
}
