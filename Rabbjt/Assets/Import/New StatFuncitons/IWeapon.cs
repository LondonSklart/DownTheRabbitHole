using System.Collections.Generic;




public interface IWeapon
{
     List<BaseStat> Stats { get; set; }
    bool[] AOE { get; set; }
    void PerformAttack();
   
}  