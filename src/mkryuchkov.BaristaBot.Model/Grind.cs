using UnitsNet;

namespace mkryuchkov.BaristaBot.Model;

public struct Grind : IQuantity<Grinder, float>
{
    public Grind(float value, Grinder grinder)
    {
        Value = value;
        Unit = grinder;
    }

    public Grinder Unit { get; }
    Enum IQuantity.Unit => Unit;

    public float Value { get; }
    QuantityValue IQuantity.Value => Value;
    
    public BaseDimensions Dimensions => BaseDimensions.Dimensionless;

    private static QuantityInfo<Grinder> QInfo => new(nameof(Grinder),
        new[]
        {
            new UnitInfo<Grinder>(Grinder.CommandanteC40, "CCls", BaseUnits.Undefined),
            new UnitInfo<Grinder>(Grinder.CommandanteC40RedClix, "CRCls", BaseUnits.Undefined),
        },
        Grinder.CommandanteC40,
        new Grind(20, Grinder.CommandanteC40),
        BaseDimensions.Dimensionless
    );

    public QuantityInfo<Grinder> QuantityInfo => QInfo;
    QuantityInfo IQuantity.QuantityInfo => QInfo;


    static Grind()
    {
        // todo: fill static fields
        // todo: RegisterDefaultConversions
        // var unitConverter = UnitConverter.Default;
        // unitConverter.SetConversionFunction<HowMuch>(HowMuchUnit.Lots, HowMuchUnit.Some, x => new HowMuch(x.Value * 2, HowMuchUnit.Some));
        
        // todo: MapGeneratedLocalizations
        // unitAbbreviationsCache.PerformAbbreviationMapping(TemperatureUnit.DegreeCelsius, new CultureInfo("en-US"), false, true, new string[]{"°C"});
        // Map unit enum values to unit abbreviations
        // UnitAbbreviationsCache.Default.MapUnitToDefaultAbbreviation(HowMuchUnit.Some, "sm");
        UnitAbbreviationsCache.Default.MapUnitToDefaultAbbreviation(Grinder.CommandanteC40, "CCl");
        UnitAbbreviationsCache.Default.MapUnitToDefaultAbbreviation(Grinder.CommandanteC40RedClix, "CRCl");
        UnitAbbreviationsCache.Default.MapUnitToDefaultAbbreviation(Grinder.CommandanteC40RedClix, "CRCl");
    }

    public IValueQuantity<float> ToUnit(Enum unit)
    {
        throw new NotImplementedException();
    }

    IValueQuantity<float> IValueQuantity<float>.ToUnit(UnitSystem unitSystem)
    {
        throw new NotImplementedException();
    }

    IQuantity<Grinder> IQuantity<Grinder>.ToUnit(UnitSystem unitSystem)
    {
        throw new NotImplementedException();
    }

    double IQuantity.As(Enum unit)
    {
        throw new NotImplementedException();
    }

    float IValueQuantity<float>.As(UnitSystem unitSystem)
    {
        throw new NotImplementedException();
    }

    public float As(Enum unit)
    {
        throw new NotImplementedException();
    }

    double IQuantity.As(UnitSystem unitSystem)
    {
        throw new NotImplementedException();
    }

    IQuantity IQuantity.ToUnit(Enum unit)
    {
        return ToUnit(unit);
    }

    double IQuantity<Grinder>.As(Grinder unit)
    {
        throw new NotImplementedException();
    }

    float IQuantity<Grinder, float>.As(Grinder unit)
    {
        throw new NotImplementedException();
    }

    public IQuantity<Grinder> ToUnit(Grinder unit)
    {
        throw new NotImplementedException();
    }

    IQuantity IQuantity.ToUnit(UnitSystem unitSystem)
    {
        throw new NotImplementedException();
    }
    
    public override string ToString() => $"{Value} {UnitAbbreviationsCache.Default.GetDefaultAbbreviation(Unit)}";
    public string ToString(string? format, IFormatProvider? formatProvider) => $"{format} {formatProvider}";
    public string ToString(IFormatProvider? provider) => $"{provider}";
}