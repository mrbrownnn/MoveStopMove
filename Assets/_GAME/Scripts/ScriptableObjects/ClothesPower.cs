using UnityEngine;

[CreateAssetMenu(fileName = "New Clothes", menuName = "Scriptable Objects/ClothesPower")]
public class ClothesPower:ScriptableObject
{
    // pay to win function :))))
    [HideInInspector] public enum AddPowerType { addition, multiplication }
    [Header("Arrow Power")]
    public float AddArrowAttackRange;
    public AddPowerType addArrowRangeType;
    public float AddArrowMoveSpeed;
    public AddPowerType addArrowMoveSpeedType;
    public float AddArrowGold;
    public AddPowerType addArrowGoldType;

    [Header("Cowboy Power")]
    public float AddCowboyAttackRange;
    public AddPowerType addCowboyRangeType;
    public float AddCowboyMoveSpeed;
    public AddPowerType addCowboyMoveSpeedType;
    public float AddCowboyGold;
    public AddPowerType addCowboyGoldType;

    [Header("Crown Power")]
    public float AddCrownAttackRange;
    public AddPowerType addCrownRangeType;
    public float AddCrownMoveSpeed;
    public AddPowerType addCrownMoveSpeedType;
    public float AddCrownGold;
    public AddPowerType addCrownGoldType;

    [Header("Ear Power")]
    public float AddEarAttackRange;
    public AddPowerType addEarRangeType;
    public float AddEarMoveSpeed;
    public AddPowerType addEarMoveSpeedType;
    public float AddEarGold;
    public AddPowerType addEarGoldType;

    [Header("Hat Power")]
    public float AddHatAttackRange;
    public AddPowerType addHatRangeType;
    public float AddHatMoveSpeed;
    public AddPowerType addHatMoveSpeedType;
    public float AddHatGold;
    public AddPowerType addHatGoldType;
    
    [Header("HatCap Power")]
    public float AddHatCapAttackRange;
    public AddPowerType addHatCapRangeType;
    public float AddHatCapMoveSpeed;
    public AddPowerType addHatCapMoveSpeedType;
    public float AddHatCapGold;
    public AddPowerType addHatCapGoldType;
    
    [Header("HatYellow Power")]
    public float AddHatYellowAttackRange;
    public AddPowerType addHatYellowRangeType;
    public float AddHatYellowMoveSpeed;
    public AddPowerType addHatYellowMoveSpeedType;
    public float AddHatYellowGold;
    public AddPowerType addHatYellowGoldType;
    
    [Header("HeadPhone Power")]
    public float AddHeadPhoneAttackRange;
    public AddPowerType addHeadPhoneRangeType;
    public float AddHeadPhoneMoveSpeed;
    public AddPowerType addHeadPhoneMoveSpeedType;
    public float AddHeadPhoneGold;
    public AddPowerType addHeadPhoneGoldType;
    
    [Header("Rau Power")]
    public float AddRauAttackRange;
    public AddPowerType addRauRangeType;
    public float AddRauMoveSpeed;
    public AddPowerType addRauMoveSpeedType;
    public float AddRauGold;
    public AddPowerType addRauGoldType;
    
    [Header("Khien Power")]
    public float AddKhienAttackRange;
    public AddPowerType addKhienRangeType;
    public float AddKhienMoveSpeed;
    public AddPowerType addKhienMoveSpeedType;
    public float AddKhienGold;
    public AddPowerType addKhienGoldType;
    
    [Header("Shield Power")]
    public float AddShieldAttackRange;
    public AddPowerType addShieldRangeType;
    public float AddShieldMoveSpeed;
    public AddPowerType addShieldMoveSpeedType;
    public float AddShieldGold;
    public AddPowerType addShieldGoldType;
    
    [Header("Batman Power")]
    public float AddBatmanAttackRange;
    public AddPowerType addBatmanRangeType;
    public float AddBatmanMoveSpeed;
    public AddPowerType addBatmanMoveSpeedType;
    public float AddBatmanGold;
    public AddPowerType addBatmanGoldType;
    
    [Header("Chambi Power")]
    public float AddChambiAttackRange;
    public AddPowerType addChambiRangeType;
    public float AddChambiMoveSpeed;
    public AddPowerType addChambiMoveSpeedType;
    public float AddChambiGold;
    public AddPowerType addChambiGoldType;
    
    [Header("Comy Power")]
    public float AddComyAttackRange;
    public AddPowerType addComyRangeType;
    public float AddComyMoveSpeed;
    public AddPowerType addComyMoveSpeedType;
    public float AddComyGold;
    public AddPowerType addComyGoldType;
    
    [Header("Dabao Power")]
    public float AddDabaoAttackRange;
    public AddPowerType addDabaoRangeType;
    public float AddDabaoMoveSpeed;
    public AddPowerType addDabaoMoveSpeedType;
    public float AddDabaoGold;
    public AddPowerType addDabaoGoldType;
    
    [Header("Onion Power")]
    public float AddOnionAttackRange;
    public AddPowerType addOnionRangeType;
    public float AddOnionMoveSpeed;
    public AddPowerType addOnionMoveSpeedType;
    public float AddOnionGold;
    public AddPowerType addOnionGoldType;
    
    [Header("Pokemon Power")]
    public float AddPokemonAttackRange;
    public AddPowerType addPokemonRangeType;
    public float AddPokemonMoveSpeed;
    public AddPowerType addPokemonMoveSpeedType;
    public float AddPokemonGold;
    public AddPowerType addPokemonGoldType;
    
    [Header("RainBow Power")]
    public float AddRainBowAttackRange;
    public AddPowerType addRainBowRangeType;
    public float AddRainBowMoveSpeed;
    public AddPowerType addRainBowMoveSpeedType;
    public float AddRainBowGold;
    public AddPowerType addRainBowGoldType;
    
    [Header("Skull Power")]
    public float AddSkullAttackRange;
    public AddPowerType addSkullRangeType;
    public float AddSkullMoveSpeed;
    public AddPowerType addSkullMoveSpeedType;
    public float AddSkullGold;
    public AddPowerType addSkullGoldType;
    
    [Header("Vantim Power")]
    public float AddVantimAttackRange;
    public AddPowerType addVantimRangeType;
    public float AddVantimMoveSpeed;
    public AddPowerType addVantimMoveSpeedType;
    public float AddVantimGold;
    public AddPowerType addVantimGoldType;
    
    [Header("Devil Power")]
    public float AddDevilAttackRange;
    public AddPowerType addDevilRangeType;
    public float AddDevilMoveSpeed;
    public AddPowerType addDevilMoveSpeedType;
    public float AddDevilGold;
    public AddPowerType addDevilGoldType;
    
    [Header("Angel Power")]
    public float AddAngelAttackRange;
    public AddPowerType addAngelRangeType;
    public float AddAngelMoveSpeed;
    public AddPowerType addAngelMoveSpeedType;
    public float AddAngelGold;
    public AddPowerType addAngelGoldType;
    
    [Header("Witch Power")]
    public float AddWitchAttackRange;
    public AddPowerType addWitchRangeType;
    public float AddWitchMoveSpeed;
    public AddPowerType addWitchMoveSpeedType;
    public float AddWitchGold;
    public AddPowerType addWitchGoldType;
    
    [Header("Deadpool Power")]
    public float AddDeadpoolAttackRange;
    public AddPowerType addDeadpoolRangeType;
    public float AddDeadpoolMoveSpeed;
    public AddPowerType addDeadpoolMoveSpeedType;
    public float AddDeadpoolGold;
    public AddPowerType addDeadpoolGoldType;
    
    [Header("Thor Power")]
    public float AddThorAttackRange;
    public AddPowerType addThorRangeType;
    public float AddThorMoveSpeed;
    public AddPowerType addThorMoveSpeedType;
    public float AddThorGold;
    public AddPowerType addThorGoldType;
}
