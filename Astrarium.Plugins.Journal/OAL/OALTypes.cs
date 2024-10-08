﻿using Astrarium.Plugins.Journal.Types;
using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Astrarium.Plugins.Journal.OAL
{
    /// <summary>
    /// Root element for OAL data
    /// </summary>
    [Serializable]
    [XmlType(AnonymousType = true, Namespace = OAL)]
    [XmlRoot(ElementName = "observations", Namespace = OAL, IsNullable = false)]
    public class OALData
    {
        /// <summary>
        /// OAL scheme URI
        /// </summary>
        public const string OAL = "http://groups.google.com/group/openastronomylog";

        /// <summary>
        /// XSI scheme URI
        /// </summary>
        public const string XSI = "http://www.w3.org/2001/XMLSchema-instance";

        /// <summary>
        /// Schema location
        /// </summary>
        [XmlAttribute(AttributeName = "schemaLocation", Namespace = XSI)]
        public string SchemaLocation { get; set; } = $"{OAL} oal21.xsd";

        /// <summary>
        /// OAL document version (use 2.1 for export)
        /// </summary>
        [XmlAttribute(AttributeName = "version", Namespace = "")]
        public string Version { get; set; } = "2.1";

        /// <summary>
        /// Observers and coobservers
        /// </summary>
        [XmlArray(ElementName = "observers", Namespace = "")]
        [XmlArrayItem(ElementName = "observer", Namespace = "", IsNullable = false)]
        public OALObserver[] Observers { get; set; }

        /// <summary>
        /// Sites (places) of observation
        /// </summary>
        [XmlArray(ElementName = "sites", Namespace = "")]
        [XmlArrayItem(ElementName = "site", Namespace = "", IsNullable = false)]
        public OALSite[] Sites { get; set; }

        /// <summary>
        /// Sessions (series of observations)
        /// </summary>
        [XmlArray(ElementName = "sessions", Namespace = "")]
        [XmlArrayItem(ElementName = "session", Namespace = "", IsNullable = false)]
        public OALSession[] Sessions { get; set; }

        /// <summary>
        /// Observation targets (celestial objects)
        /// </summary>
        [XmlArray(ElementName = "targets", Namespace = "")]
        [XmlArrayItem(ElementName = "target", Namespace = "", IsNullable = false)]
        public OALTarget[] Targets { get; set; }

        /// <summary>
        /// Optical devices (telescopes, binoculars, optical tubes etc.)
        /// </summary>
        [XmlArray(ElementName = "scopes", Namespace = "")]
        [XmlArrayItem(ElementName = "scope", Namespace = "", IsNullable = false)]
        public OALOptics[] Optics { get; set; }

        /// <summary>
        /// Eyepieces
        /// </summary>
        [XmlArray(ElementName = "eyepieces", Namespace = "")]
        [XmlArrayItem(ElementName = "eyepiece", Namespace = "", IsNullable = false)]
        public OALEyepiece[] Eyepieces { get; set; }

        /// <summary>
        /// Lenses
        /// </summary>
        [XmlArray(ElementName = "lenses", Namespace = "")]
        [XmlArrayItem(ElementName = "lens", Namespace = "", IsNullable = false)]
        public OALLens[] Lenses { get; set; }

        /// <summary>
        /// Filters
        /// </summary>
        [XmlArray(ElementName = "filters", Namespace = "")]
        [XmlArrayItem(ElementName = "filter", Namespace = "", IsNullable = false)]
        public OALFilter[] Filters { get; set; }

        /// <summary>
        /// Cameras
        /// </summary>
        [XmlArray(ElementName = "imagers", Namespace = "")]
        [XmlArrayItem(ElementName = "imager", Namespace = "", IsNullable = false)]
        public OALImager[] Cameras { get; set; }

        /// <summary>
        /// Observations
        /// </summary>
        [XmlElement(ElementName = "observation", Namespace = "")]
        public OALObservation[] Observations { get; set; }
    }

    /// <summary>
    /// Observer: a person who makes observations
    /// </summary>
    [Serializable]
    [XmlType]
    public class OALObserver
    {
        /// <summary>
        /// Observer name
        /// </summary>
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Observer surname
        /// </summary>
        [XmlElement(ElementName = "surname")]
        public string Surname { get; set; }

        /// <summary>
        /// Contacts of the observer (for example, e-mail, address, phone and etc.)
        /// </summary>
        [XmlElement(ElementName = "contact")]
        public string[] Contact { get; set; }

        /// <summary>
        /// Deprecated element. Please use account element instead.
        /// Abbreviation/code of the observer for the Deep Sky Liste (DSL) of the Fachgruppe.
        /// </summary> 
        [XmlElement(ElementName = "DSL")]
        public string DSL { get; set; }

        /// <summary>
        /// Observer's accounts in different astronomical services
        /// </summary>
        [XmlElement(ElementName = "account")]
        public OALObserverAccount[] Account { get; set; }

        /// <summary>
        /// Personal offset to the "reference" correlation between the sky quality 
        /// as it can be measured with an SQM and the estimated naked eye limiting magnitude(fst) 
        /// The individual observer's offset depends mainly on the visual acuity of the observer. 
        /// If the fstOffset is known, the sky quality may be derived from faintestStar estimates
        /// by this observer. 
        /// The "reference" correlation used to convert between sky quality and fst was given by 
        /// Bradley Schaefer: fst = 5 * (1.586 - log(10 ^ ((21.568 - BSB) / 5) + 1)) where BSB is the sky quality
        /// (or background surface brightness) given in magnitudes per square arcsecond 
        /// </summary>
        [XmlElement(ElementName = "fstOffset")]
        public double FSTOffset { get; set; }

        /// <summary>
        /// Flag indicating <see cref="FSTOffset"/> value is specified
        /// </summary>
        [XmlIgnore]
        public bool FSTOffsetSpecified { get; set; }

        /// <summary>
        /// Observer id
        /// </summary>
        [XmlAttribute(AttributeName = "id", DataType = "ID")]
        public string Id { get; set; }
    }

    /// <summary>
    /// Defines account of an observer in some astronomical resource,
    /// for example, login name at CloudyNights
    /// </summary>
    [Serializable]
    [XmlType]
    public class OALObserverAccount
    {
        /// <summary>
        /// Name of service/resource, like "cloudynights.com"
        /// </summary>
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Name of the observer account in thw service/resource
        /// </summary>
        [XmlText]
        public string Value { get; set; }
    }

    /// <summary>
    /// Type for variable star chart identification
    /// </summary>
    [Serializable]
    [XmlType(TypeName = "variableStarChartIDType")]
    public class OALVariableStarChartId
    {
        /// <summary>
        /// Flag indicating the <see cref="Value"/> is non-AAVSO chart number
        /// </summary>
        [XmlAttribute(AttributeName = "nonAAVSOchart")]
        public bool NonAAVSOChart { get; set; }

        /// <summary>
        /// Optional attribute if given chart is not an AAVSO chart
        /// </summary>
        [XmlIgnore]
        public bool NonAAVSOChartSpecified { get; set; }

        /// <summary>
        /// Variable star chart identification
        /// </summary>
        [XmlText]
        public string Value { get; set; }
    }

    /// <summary>
    /// Type for variable star visual magnitude
    /// </summary>
    [Serializable]
    [XmlType(TypeName = "variableStarVisMagType")]
    public class OALVariableStarVisMag
    {
        /// <summary>
        /// Optional attribute if given visual magnitude is a fainter than value
        /// </summary>
        [XmlAttribute(AttributeName = "fainterThan")]
        public bool FainterThan { get; set; }

        /// <summary>
        /// Flag indicating <see cref="FainterThan" /> is specified
        /// </summary>
        [XmlIgnore]
        public bool FainterThanSpecified { get; set; }

        /// <summary>
        /// Optional attribute if given visual magnitude is uncertain
        /// </summary>
        [XmlAttribute(AttributeName = "uncertain")]
        public bool Uncertain { get; set; }

        /// <summary>
        /// Flag indicating <see cref="Uncertain" /> is specified
        /// </summary>
        [XmlIgnore]
        public bool UncertainSpecified { get; set; }

        /// <summary>
        /// Value of visual magnitude
        /// </summary>
        [XmlText]
        public double Value { get; set; }
    }

    /// <summary>
    /// Base class for all findings (results of observations)
    /// </summary>
    [XmlInclude(typeof(OALFindingsVariableStar))]
    [XmlInclude(typeof(OALFindingsDeepSky))]
    [XmlInclude(typeof(OALFindingsDeepSkyDS))]
    [XmlInclude(typeof(OALFindingsDeepSkyOC))]
    [Serializable]
    [XmlType(TypeName = "findingsType", Namespace = OALData.OAL)]
    public class OALFindings
    {
        /// <summary>
        /// Description of the finding
        /// </summary>
        [XmlElement(ElementName = "description", Form = XmlSchemaForm.Unqualified)]
        public string Description { get; set; }

        /// <summary>
        /// Language of the description (2-letter code)
        /// </summary>
        [XmlAttribute(AttributeName = "lang")]
        public string Lang { get; set; }
    }

    [CelestialObjectType("Nova")]
    [CelestialObjectType("VarStar")]
    [Serializable]
    [XmlType(TypeName = "findingsVariableStarType", Namespace = OALData.OAL)]
    public class OALFindingsVariableStar : OALFindings
    {
        [OALConverter(ExportConverter = typeof(ExportVariableStarVisMagConverter), ImportConverter = typeof(ImportVariableStarVisMagConverter))]
        [XmlElement(ElementName = "visMag", Form = XmlSchemaForm.Unqualified)]
        public OALVariableStarVisMag VisMag { get; set; }

        [OALConverter(Property = nameof(VariableStarObservationDetails.ComparisonStars), ExportConverter = typeof(ExportJsonConverter), ImportConverter = typeof(ImportJsonConverter))]
        [XmlElement(ElementName = "comparisonStar", Form = XmlSchemaForm.Unqualified)]
        public string[] ComparisonStar { get; set; }

        [OALConverter(ExportConverter = typeof(ExportVariableStarChartIdconverter), ImportConverter = typeof(ImportVariableStarChartIdconverter))]
        [XmlElement(ElementName = "chartID", Form = XmlSchemaForm.Unqualified)]
        public OALVariableStarChartId ChartId { get; set; }

        [OALConverter(Property = nameof(VariableStarObservationDetails.BrightSky), ExportConverter = typeof(ExportNullableBoolConverter), ImportConverter = typeof(SimpleConverter))]
        [XmlAttribute(AttributeName = "brightSky")]
        public bool BrightSky { get; set; }

        [XmlIgnore]
        public bool BrightSkySpecified { get; set; }

        [OALConverter(Property = nameof(VariableStarObservationDetails.Clouds), ExportConverter = typeof(ExportNullableBoolConverter), ImportConverter = typeof(SimpleConverter))]
        [XmlAttribute(AttributeName = "clouds")]
        public bool Clouds { get; set; }

        [XmlIgnore]
        public bool CloudsSpecified { get; set; }

        [OALConverter(Property = nameof(VariableStarObservationDetails.PoorSeeing), ExportConverter = typeof(ExportNullableBoolConverter), ImportConverter = typeof(SimpleConverter))]
        [XmlAttribute(AttributeName = "poorSeeing")]
        public bool PoorSeeing { get; set; }

        [XmlIgnore]
        public bool PoorSeeingSpecified { get; set; }

        [OALConverter(Property = nameof(VariableStarObservationDetails.NearHorizion), ExportConverter = typeof(ExportNullableBoolConverter), ImportConverter = typeof(SimpleConverter))]
        [XmlAttribute(AttributeName = "nearHorizion")]
        public bool NearHorizion { get; set; }

        [XmlIgnore]
        public bool NearHorizionSpecified { get; set; }

        [OALConverter(Property = nameof(VariableStarObservationDetails.UnusualActivity), ExportConverter = typeof(ExportNullableBoolConverter), ImportConverter = typeof(SimpleConverter))]
        [XmlAttribute(AttributeName = "unusualActivity")]
        public bool UnusualActivity { get; set; }

        [XmlIgnore]
        public bool UnusualActivitySpecified { get; set; }

        [OALConverter(Property = nameof(VariableStarObservationDetails.Outburst), ExportConverter = typeof(ExportNullableBoolConverter), ImportConverter = typeof(SimpleConverter))]
        [XmlAttribute(AttributeName = "outburst")]
        public bool Outburst { get; set; }

        [XmlIgnore]
        public bool OutburstSpecified { get; set; }

        [OALConverter(Property = nameof(VariableStarObservationDetails.ComparismSequenceProblem), ExportConverter = typeof(ExportNullableBoolConverter), ImportConverter = typeof(SimpleConverter))]
        [XmlAttribute(AttributeName = "comparismSequenceProblem")]
        public bool ComparismSequenceProblem { get; set; }

        [XmlIgnore]
        public bool ComparismSequenceProblemSpecified { get; set; }

        [OALConverter(Property = nameof(VariableStarObservationDetails.StarIdentificationUncertain), ExportConverter = typeof(ExportNullableBoolConverter), ImportConverter = typeof(SimpleConverter))]
        [XmlAttribute(AttributeName = "starIdentificationUncertain")]
        public bool StarIdentificationUncertain { get; set; }

        [XmlIgnore]
        public bool StarIdentificationUncertainSpecified { get; set; }

        [OALConverter(Property = nameof(VariableStarObservationDetails.FaintStar), ExportConverter = typeof(ExportNullableBoolConverter), ImportConverter = typeof(SimpleConverter))]
        [XmlAttribute(AttributeName = "faintStar")]
        public bool FaintStar { get; set; }

        [XmlIgnore]
        public bool FaintStarSpecified { get; set; }
    }

    [CelestialObjectType("DeepSky.PlanetaryNebula")]
    [CelestialObjectType("DeepSky.Asterism")]
    [CelestialObjectType("DeepSky.GalaxyCluster")]
    [CelestialObjectType("DeepSky.DarkNebula")]
    [CelestialObjectType("DeepSky.GalacticNebula")]
    [CelestialObjectType("DeepSky.Galaxy")]
    [CelestialObjectType("DeepSky.GlobularCluster")]
    [CelestialObjectType("DeepSky.MultipleStar")]
    [CelestialObjectType("DeepSky.Quasar")]
    [CelestialObjectType("DeepSky.StarCloud")]
    [CelestialObjectType("DeepSky.Unspecified")]
    [XmlInclude(typeof(OALFindingsDeepSkyDS))]
    [XmlInclude(typeof(OALFindingsDeepSkyOC))]
    [Serializable]
    [XmlType(TypeName = "findingsDeepSkyType", Namespace = OALData.OAL)]
    public class OALFindingsDeepSky : OALFindings
    {
        [OALConverter(Property = nameof(DeepSkyObservationDetails.SmallDiameter), ExportConverter = typeof(ExportArcSecondsConverter), ImportConverter = typeof(ImportArcSecondsConverter))]
        [XmlElement(ElementName = "smallDiameter", Form = XmlSchemaForm.Unqualified)]
        public OALNonNegativeAngle SmallDiameter { get; set; }

        [OALConverter(Property = nameof(DeepSkyObservationDetails.LargeDiameter), ExportConverter = typeof(ExportArcSecondsConverter), ImportConverter = typeof(ImportArcSecondsConverter))]
        [XmlElement(ElementName = "largeDiameter", Form = XmlSchemaForm.Unqualified)]
        public OALNonNegativeAngle LargeDiameter { get; set; }

        [OALConverter(Property = nameof(DeepSkyObservationDetails.Rating), ExportConverter = typeof(ExportDeepSkyRatingConverter), ImportConverter = typeof(ImportEnumAsStringConverter))]
        [XmlElement(ElementName = "rating", Form = XmlSchemaForm.Unqualified)]
        public OALFindingsDeepSkyRating Rating { get; set; }

        [OALConverter(Property = nameof(DeepSkyObservationDetails.Stellar), ExportConverter = typeof(ExportNullableBoolConverter), ImportConverter = typeof(SimpleConverter))]
        [XmlAttribute(AttributeName = "stellar")]
        public bool Stellar { get; set; }

        [XmlIgnore]
        public bool StellarSpecified { get; set; }

        [OALConverter(Property = nameof(DeepSkyObservationDetails.Extended), ExportConverter = typeof(ExportNullableBoolConverter), ImportConverter = typeof(SimpleConverter))]
        [XmlAttribute(AttributeName = "extended")]
        public bool Extended { get; set; }

        [XmlIgnore]
        public bool ExtendedSpecified { get; set; }

        [OALConverter(Property = nameof(DeepSkyObservationDetails.Resolved), ExportConverter = typeof(ExportNullableBoolConverter), ImportConverter = typeof(SimpleConverter))]
        [XmlAttribute(AttributeName = "resolved")]
        public bool Resolved { get; set; }

        [XmlIgnore]
        public bool ResolvedSpecified { get; set; }

        [OALConverter(Property = nameof(DeepSkyObservationDetails.Mottled), ExportConverter = typeof(ExportNullableBoolConverter), ImportConverter = typeof(SimpleConverter))]
        [XmlAttribute(AttributeName = "mottled")]
        public bool Mottled { get; set; }

        [XmlIgnore]
        public bool MottledSpecified { get; set; }
    }

    [Serializable]
    [XmlType(TypeName = "nonNegativeAngleType", Namespace = OALData.OAL)]
    public class OALNonNegativeAngle : OALAngle { }

    [XmlInclude(typeof(OALNonNegativeAngle))]
    [Serializable]
    [XmlType(TypeName = "angleType", Namespace = OALData.OAL)]
    public class OALAngle
    {
        [XmlAttribute(AttributeName = "unit")]
        public OALAngleUnit Unit { get; set; }

        [XmlText]
        public double Value { get; set; }
    }

    [Serializable]
    [XmlType(TypeName = "angleUnit", Namespace = OALData.OAL)]
    public enum OALAngleUnit
    {
        [XmlEnum("arcsec")]
        ArcSec,

        [XmlEnum("arcmin")]
        ArcMin,

        [XmlEnum("deg")]
        Deg,

        [XmlEnum("rad")]
        Rad,
    }

    /// <summary>
    /// Deep sky object's visual rating according to the scale of the "Deep Sky Liste"
    /// </summary>
    [Serializable]
    [XmlType(AnonymousType = true, Namespace = OALData.OAL)]
    public enum OALFindingsDeepSkyRating
    {
        [XmlEnum("1")]
        Rating1,

        [XmlEnum("2")]
        Rating2,

        [XmlEnum("3")]
        Rating3,

        [XmlEnum("4")]
        Rating4,

        [XmlEnum("5")]
        Rating5,

        [XmlEnum("6")]
        Rating6,

        [XmlEnum("7")]
        Rating7,

        [XmlEnum("99")]
        Unknown,
    }

    [CelestialObjectType("DeepSky.DoubleStar")]
    [Serializable]
    [XmlType(TypeName = "findingsDeepSkyDSType", Namespace = OALData.OAL)]
    public class OALFindingsDeepSkyDS : OALFindingsDeepSky
    {
        [OALConverter(Property = nameof(DoubleStarObservationDetails.ColorMainComponent), ExportConverter = typeof(ExportStarColorConverter), ImportConverter = typeof(ImportEnumAsStringConverter))]
        [XmlElement(ElementName = "colorMain", Form = XmlSchemaForm.Unqualified)]
        public OALStarColor ColorMain { get; set; }

        [XmlIgnore]
        public bool ColorMainSpecified { get; set; }

        [OALConverter(Property = nameof(DoubleStarObservationDetails.ColorCompanionComponent), ExportConverter = typeof(ExportStarColorConverter), ImportConverter = typeof(ImportEnumAsStringConverter))]
        [XmlElement(ElementName = "colorCompanion", Form = XmlSchemaForm.Unqualified)]
        public OALStarColor ColorCompanion { get; set; }

        [XmlIgnore]
        public bool ColorCompanionSpecified { get; set; }

        [OALConverter(Property = nameof(DoubleStarObservationDetails.EqualBrightness), ExportConverter = typeof(ExportNullableBoolConverter), ImportConverter = typeof(SimpleConverter))]
        [XmlAttribute(AttributeName = "equalBrightness")]
        public bool EqualBrightness { get; set; }

        [XmlIgnore]
        public bool EqualBrightnessSpecified { get; set; }

        [OALConverter(Property = nameof(DoubleStarObservationDetails.NiceSurrounding), ExportConverter = typeof(ExportNullableBoolConverter), ImportConverter = typeof(SimpleConverter))]
        [XmlAttribute(AttributeName = "niceSurrounding")]
        public bool NiceSurrounding { get; set; }

        [XmlIgnore]
        public bool NiceSurroundingSpecified { get; set; }
    }

    [Serializable]
    [XmlType(TypeName = "starColorType", Namespace = OALData.OAL)]
    public enum OALStarColor
    {
        [XmlEnum("white")]
        White,

        [XmlEnum("red")]
        Red,

        [XmlEnum("orange")]
        Orange,

        [XmlEnum("yellow")]
        Yellow,

        [XmlEnum("green")]
        Green,

        [XmlEnum("blue")]
        Blue,
    }

    [CelestialObjectType("DeepSky.OpenCluster")]
    [Serializable]
    [XmlType(TypeName = "findingsDeepSkyOCType", Namespace = OALData.OAL)]
    public class OALFindingsDeepSkyOC : OALFindingsDeepSky
    {
        [OALConverter(Property = nameof(OpenClusterObservationDetails.Character), ExportConverter = typeof(ExportClusterCharacterConverter), ImportConverter = typeof(ImportEnumAsStringConverter))]
        [XmlElement(ElementName = "character", Form = XmlSchemaForm.Unqualified)]
        public OALClusterCharacter Character { get; set; }

        [XmlIgnore]
        public bool CharacterSpecified { get; set; }

        [OALConverter(Property = nameof(OpenClusterObservationDetails.UnusualShape), ExportConverter = typeof(ExportNullableBoolConverter), ImportConverter = typeof(SimpleConverter))]
        [XmlAttribute(AttributeName = "unusualShape")]
        public bool UnusualShape { get; set; }

        [XmlIgnore]
        public bool UnusualShapeSpecified { get; set; }

        [OALConverter(Property = nameof(OpenClusterObservationDetails.PartlyUnresolved), ExportConverter = typeof(ExportNullableBoolConverter), ImportConverter = typeof(SimpleConverter))]
        [XmlAttribute(AttributeName = "partlyUnresolved")]
        public bool PartlyUnresolved { get; set; }

        [XmlIgnore]
        public bool PartlyUnresolvedSpecified { get; set; }

        [OALConverter(Property = nameof(OpenClusterObservationDetails.ColorContrasts), ExportConverter = typeof(ExportNullableBoolConverter), ImportConverter = typeof(SimpleConverter))]
        [XmlAttribute(AttributeName = "colorContrasts")]
        public bool ColorContrasts { get; set; }

        [XmlIgnore]
        public bool ColorContrastsSpecified { get; set; }
    }


    [Serializable]
    [XmlType(TypeName = "clusterCharacterType", Namespace = OALData.OAL)]
    public enum OALClusterCharacter
    {
        [XmlEnum("A")]
        A,

        [XmlEnum("B")]
        B,

        [XmlEnum("C")]
        C,

        [XmlEnum("D")]
        D,

        [XmlEnum("E")]
        E,

        [XmlEnum("F")]
        F,

        [XmlEnum("G")]
        G,

        [XmlEnum("H")]
        H,

        [XmlEnum("I")]
        I,

        [XmlEnum("X")]
        X
    }


    [Serializable]
    [XmlType(TypeName = "observation", Namespace = OALData.OAL)]
    public class OALObservation
    {

        [XmlElement(ElementName = "observer", Form = XmlSchemaForm.Unqualified, DataType = "IDREF")]
        public string ObserverId { get; set; }


        [XmlElement(ElementName = "site", Form = XmlSchemaForm.Unqualified, DataType = "IDREF")]
        public string SiteId { get; set; }


        [XmlElement(ElementName = "session", Form = XmlSchemaForm.Unqualified, DataType = "IDREF")]
        public string SessionId { get; set; }


        [XmlElement(ElementName = "target", Form = XmlSchemaForm.Unqualified, DataType = "IDREF")]
        public string TargetId { get; set; }

        [XmlElement(ElementName = "begin", Form = XmlSchemaForm.Unqualified)]
        public string Begin { get; set; }

        [XmlElement(ElementName = "end", Form = XmlSchemaForm.Unqualified)]
        public string End { get; set; }

        [XmlIgnore]
        public bool EndSpecified { get; set; }

        [XmlElement(ElementName = "faintestStar", Form = XmlSchemaForm.Unqualified)]
        public double FaintestStar { get; set; }


        [XmlIgnore]
        public bool FaintestStarSpecified { get; set; }


        [XmlElement(ElementName = "sky-quality", Form = XmlSchemaForm.Unqualified)]
        public OALSurfaceBrightness SkyQuality { get; set; }


        [XmlElement(ElementName = "seeing", Form = XmlSchemaForm.Unqualified, DataType = "nonNegativeInteger")]
        public string Seeing { get; set; }


        [XmlElement(ElementName = "scope", Form = XmlSchemaForm.Unqualified, DataType = "IDREF")]
        public string ScopeId { get; set; }


        [XmlElement(ElementName = "accessories", Form = XmlSchemaForm.Unqualified)]
        public string Accessories { get; set; }


        [XmlElement(ElementName = "eyepiece", Form = XmlSchemaForm.Unqualified, DataType = "IDREF")]
        public string EyepieceId { get; set; }


        [XmlElement(ElementName = "lens", Form = XmlSchemaForm.Unqualified, DataType = "IDREF")]
        public string LensId { get; set; }


        [XmlElement(ElementName = "filter", Form = XmlSchemaForm.Unqualified, DataType = "IDREF")]
        public string FilterId { get; set; }

        [XmlElement(ElementName = "magnification", Form = XmlSchemaForm.Unqualified)]
        public double Magnification { get; set; }

        [XmlIgnore]
        public bool MagnificationSpecified { get; set; }

        [XmlElement(ElementName = "imager", Form = XmlSchemaForm.Unqualified, DataType = "IDREF")]
        public string CameraId { get; set; }

        [XmlElement(ElementName = "result", Form = XmlSchemaForm.Unqualified)]
        public OALFindings[] Result { get; set; }

        [XmlElement(ElementName = "image", Form = XmlSchemaForm.Unqualified)]
        public string[] Images { get; set; }

        [XmlAttribute(AttributeName = "id", DataType = "ID")]
        public string Id { get; set; }
    }


    [Serializable]
    [XmlType(TypeName = "surfaceBrightnessType", Namespace = OALData.OAL)]
    public class OALSurfaceBrightness
    {

        [XmlAttribute(AttributeName = "unit")]
        public OALSurfaceBrightnessUnit Unit { get; set; }

        [XmlText]
        public double Value { get; set; }
    }


    [Serializable]
    [XmlType(TypeName = "surfaceBrightnessUnit", Namespace = OALData.OAL)]
    public enum OALSurfaceBrightnessUnit
    {
        [XmlEnum("mags-per-squarearcsec")]
        MagsPerSquareArcSec,

        [XmlEnum("mags-per-squarearcmin")]
        MagsPerSquareArcMin,
    }

    /// <remarks />
    [XmlInclude(typeof(OALCamera))]
    [Serializable]
    [XmlType(TypeName = "imagerType", Namespace = OALData.OAL)]
    public abstract class OALImager
    {
        [XmlElement(ElementName = "model", Form = XmlSchemaForm.Unqualified)]
        public string Model { get; set; }

        [XmlElement(ElementName = "vendor", Form = XmlSchemaForm.Unqualified)]
        public string Vendor { get; set; }

        [XmlElement(ElementName = "remarks", Form = XmlSchemaForm.Unqualified)]
        public string Remarks { get; set; }

        [XmlAttribute(AttributeName = "id", DataType = "ID")]
        public string Id { get; set; }
    }

    [Serializable]
    [XmlType(TypeName = "ccdCameraType", Namespace = OALData.OAL)]
    public class OALCamera : OALImager
    {
        [XmlElement(ElementName = "pixelsX", Form = XmlSchemaForm.Unqualified, DataType = "positiveInteger")]
        public string PixelsX { get; set; }

        [XmlElement(ElementName = "pixelsY", Form = XmlSchemaForm.Unqualified, DataType = "positiveInteger")]
        public string PixelsY { get; set; }

        [XmlElement(ElementName = "pixelXSize", Form = XmlSchemaForm.Unqualified)]
        public decimal PixelXSize { get; set; }

        [XmlIgnore]
        public bool PixelXSizeSpecified { get; set; }

        [XmlElement(ElementName = "pixelYSize", Form = XmlSchemaForm.Unqualified)]
        public decimal PixelYSize { get; set; }

        [XmlIgnore]
        public bool PixelYSizeSpecified { get; set; }

        [XmlElement(ElementName = "binning", Form = XmlSchemaForm.Unqualified, DataType = "integer")]
        public string Binning { get; set; } = "1";
    }

    [Serializable]
    [XmlType(TypeName = "filterType", Namespace = OALData.OAL)]
    public class OALFilter
    {
        [XmlElement(ElementName = "model", Form = XmlSchemaForm.Unqualified)]
        public string Model { get; set; }

        [XmlElement(ElementName = "vendor", Form = XmlSchemaForm.Unqualified)]
        public string Vendor { get; set; }

        [XmlElement(ElementName = "type", Form = XmlSchemaForm.Unqualified)]
        public OALFilterKind Type { get; set; }

        [XmlElement(ElementName = "color", Form = XmlSchemaForm.Unqualified)]
        public OALFilterColor Color { get; set; }

        [XmlIgnore]
        public bool ColorSpecified { get; set; }

        [XmlElement(ElementName = "wratten", Form = XmlSchemaForm.Unqualified)]
        public string Wratten { get; set; }

        [XmlElement(ElementName = "schott", Form = XmlSchemaForm.Unqualified)]
        public string Schott { get; set; }

        [XmlAttribute(AttributeName = "id", DataType = "ID")]
        public string Id { get; set; }
    }

    [Serializable]
    [XmlType(TypeName = "filterKind", Namespace = OALData.OAL)]
    public enum OALFilterKind
    {
        [XmlEnum("other")]
        Other,

        [XmlEnum("broad band")]
        Broadband,

        [XmlEnum("narrow band")]
        Narrowband,

        [XmlEnum("O-III")]
        OIII,

        [XmlEnum("H-beta")]
        HBeta,

        [XmlEnum("H-alpha")]
        HAlpha,

        [XmlEnum("color")]
        Color,

        [XmlEnum("neutral")]
        Neutral,

        [XmlEnum("corrective")]
        Corrective,

        [XmlEnum("solar")]
        Solar
    }

    [Serializable]
    [XmlType(TypeName = "filterColorType", Namespace = OALData.OAL)]
    public enum OALFilterColor
    {
        [XmlEnum("light red")]
        LightRed,

        [XmlEnum("red")]
        Red,

        [XmlEnum("deep red")]
        DeepRed,

        [XmlEnum("orange")]
        Orange,

        [XmlEnum("light yellow")]
        LightYellow,

        [XmlEnum("deep yellow")]
        DeepYellow,

        [XmlEnum("yellow")]
        Yellow,

        [XmlEnum("yellow-green")]
        YellowGreen,

        [XmlEnum("light green")]
        LightGreen,

        [XmlEnum("green")]
        Green,

        [XmlEnum("medium blue")]
        MediumBlue,

        [XmlEnum("pale blue")]
        PaleBlue,

        [XmlEnum("blue")]
        Blue,

        [XmlEnum("deep blue")]
        DeepBlue,

        [XmlEnum("violet")]
        violet,
    }

    [Serializable]
    [XmlType(TypeName = "lensType", Namespace = OALData.OAL)]
    public class OALLens
    {
        [XmlElement(ElementName = "model", Form = XmlSchemaForm.Unqualified)]
        public string Model { get; set; }

        [XmlElement(ElementName = "vendor", Form = XmlSchemaForm.Unqualified)]
        public string Vendor { get; set; }

        [XmlElement(ElementName = "factor", Form = XmlSchemaForm.Unqualified)]
        public double Factor { get; set; }

        [XmlAttribute(AttributeName = "id", DataType = "ID")]
        public string Id { get; set; }
    }

    [Serializable]
    [XmlType(TypeName = "eyepieceType", Namespace = OALData.OAL)]
    public class OALEyepiece
    {
        [XmlElement(ElementName = "model", Form = XmlSchemaForm.Unqualified)]
        public string Model { get; set; }

        [XmlElement(ElementName = "vendor", Form = XmlSchemaForm.Unqualified)]
        public string Vendor { get; set; }

        [XmlElement(ElementName = "focalLength", Form = XmlSchemaForm.Unqualified)]
        public double FocalLength { get; set; }

        [XmlElement(ElementName = "maxFocalLength", Form = XmlSchemaForm.Unqualified)]
        public double MaxFocalLength { get; set; }

        [XmlIgnore]
        public bool MaxFocalLengthSpecified { get; set; }

        [XmlElement(ElementName = "apparentFOV", Form = XmlSchemaForm.Unqualified)]
        public OALNonNegativeAngle ApparentFOV { get; set; }

        [XmlAttribute(AttributeName = "id", DataType = "ID")]
        public string Id { get; set; }
    }

    [XmlInclude(typeof(OALFixedMagnificationOptics))]
    [XmlInclude(typeof(OALScope))]
    [Serializable]
    [XmlType(TypeName = "opticsType", Namespace = OALData.OAL)]
    public abstract class OALOptics
    {
        [XmlElement(ElementName = "model", Form = XmlSchemaForm.Unqualified)]
        public string Model { get; set; }

        [XmlElement(ElementName = "type", Form = XmlSchemaForm.Unqualified)]
        public string Type { get; set; }

        [XmlElement(ElementName = "vendor", Form = XmlSchemaForm.Unqualified)]
        public string Vendor { get; set; }

        [XmlElement(ElementName = "aperture", Form = XmlSchemaForm.Unqualified)]
        public double Aperture { get; set; }

        [XmlElement(ElementName = "lightGrasp", Form = XmlSchemaForm.Unqualified)]
        public double LightGrasp { get; set; }

        [XmlIgnore]
        public bool LightGraspSpecified { get; set; }

        [XmlElement(ElementName = "orientation", Form = XmlSchemaForm.Unqualified)]
        public OALOpticsOrientation Orientation { get; set; }

        [XmlAttribute(AttributeName = "id", DataType = "ID")]
        public string Id { get; set; }
    }

    [Serializable]
    [XmlType(AnonymousType = true, Namespace = OALData.OAL)]
    public class OALOpticsOrientation
    {
        [XmlAttribute(AttributeName = "erect")]
        public bool Erect { get; set; }

        [XmlAttribute(AttributeName = "truesided")]
        public bool TrueSided { get; set; }
    }

    [Serializable]
    [XmlType(TypeName = "fixedMagnificationOpticsType", Namespace = OALData.OAL)]
    public class OALFixedMagnificationOptics : OALOptics
    {
        [XmlElement(ElementName = "magnification", Form = XmlSchemaForm.Unqualified)]
        public double Magnification { get; set; }

        [XmlElement(ElementName = "trueField", Form = XmlSchemaForm.Unqualified)]
        public OALNonNegativeAngle TrueField { get; set; }
    }

    [Serializable]
    [XmlType(TypeName = "scopeType", Namespace = OALData.OAL)]
    public class OALScope : OALOptics
    {
        [XmlElement(ElementName = "focalLength", Form = XmlSchemaForm.Unqualified)]
        public double FocalLength { get; set; }
    }

    [Serializable]
    [XmlType(TypeName = "referenceFrameType", Namespace = OALData.OAL)]
    public class OALReferenceFrame
    {
        [XmlElement(ElementName = "origin", Form = XmlSchemaForm.Unqualified)]
        public OALReferenceFrameOrigin Origin { get; set; }

        [XmlElement(ElementName = "equinox", Form = XmlSchemaForm.Unqualified)]
        public OALReferenceFrameEquinox Equinox { get; set; } = OALReferenceFrameEquinox.J2000;
    }



    [Serializable]
    [XmlType(AnonymousType = true, Namespace = OALData.OAL)]
    public enum OALReferenceFrameOrigin
    {

        [XmlEnum("geo")]
        Geo,


        [XmlEnum("topo")]
        Topo
    }



    [Serializable]
    [XmlType(AnonymousType = true, Namespace = OALData.OAL)]
    public enum OALReferenceFrameEquinox
    {
        [XmlEnum("J2000")]
        J2000,

        [XmlEnum("B1950")]
        B1950,

        [XmlEnum("EqOfDate")]
        EqOfDate,
    }

    [Serializable]
    [XmlType(TypeName = "equPosType", Namespace = OALData.OAL)]
    public class OALEquPosType
    {

        [XmlElement(ElementName = "ra", Form = XmlSchemaForm.Unqualified)]
        public OALNonNegativeAngle RA { get; set; }


        [XmlElement(ElementName = "dec", Form = XmlSchemaForm.Unqualified)]
        public OALAngle Dec { get; set; }


        [XmlElement(ElementName = "frame", Form = XmlSchemaForm.Unqualified)]
        public OALReferenceFrame Frame { get; set; }
    }


    [XmlInclude(typeof(OALTargetSolarSystem))]
    [XmlInclude(typeof(OALTargetSun))]
    [XmlInclude(typeof(OALTargetPlanet))]
    [XmlInclude(typeof(OALTargetMoon))]
    [XmlInclude(typeof(OALTargetMinorPlanet))]
    [XmlInclude(typeof(OALTargetComet))]
    [XmlInclude(typeof(OALTargetDeepSky))]
    [XmlInclude(typeof(OALTargetDeepSkySC))]
    [XmlInclude(typeof(OALTargetDeepSkyQS))]
    [XmlInclude(typeof(OALTargetDeepSkyPN))]
    [XmlInclude(typeof(OALTargetDeepSkyOC))]
    [XmlInclude(typeof(OALTargetDeepSkyNA))]
    [XmlInclude(typeof(OALTargetDeepSkyGX))]
    [XmlInclude(typeof(OALTargetDeepSkyGN))]
    [XmlInclude(typeof(OALTargetDeepSkyGC))]
    [XmlInclude(typeof(OALTargetDeepSkyDS))]
    [XmlInclude(typeof(OALTargetDeepSkyDN))]
    [XmlInclude(typeof(OALTargetDeepSkyCG))]
    [XmlInclude(typeof(OALTargetDeepSkyAS))]
    [XmlInclude(typeof(OALTargetDeepSkyMS))]
    [XmlInclude(typeof(OALTargetStar))]
    [XmlInclude(typeof(OALTargetVariableStar))]
    [Serializable]
    [XmlType(TypeName = "observationTargetType", Namespace = OALData.OAL)]
    public class OALTarget
    {
        [XmlElement("datasource", typeof(string), Form = XmlSchemaForm.Unqualified)]
        [XmlElement("observer", typeof(string), Form = XmlSchemaForm.Unqualified, DataType = "IDREF")]
        [XmlChoiceIdentifier(nameof(ItemElementName))]
        public string Item { get; set; }

        [XmlIgnore]
        public OALDataSource ItemElementName { get; set; }

        [XmlElement(ElementName = "name", Form = XmlSchemaForm.Unqualified)]
        public string Name { get; set; }

        [XmlElement(ElementName = "alias", Form = XmlSchemaForm.Unqualified)]
        public string[] Alias { get; set; }

        [XmlElement(ElementName = "position", Form = XmlSchemaForm.Unqualified)]
        public OALEquPosType Position { get; set; }

        [XmlElement(ElementName = "constellation", Form = XmlSchemaForm.Unqualified)]
        public string Constellation { get; set; }

        [XmlElement(ElementName = "notes", Form = XmlSchemaForm.Unqualified)]
        public string Notes { get; set; }

        [XmlAttribute(AttributeName = "id", DataType = "ID")]
        public string Id { get; set; }
    }

    [Serializable]
    [XmlType(Namespace = OALData.OAL, IncludeInSchema = false)]
    public enum OALDataSource
    {

        [XmlEnum(":datasource")]
        DataSource,


        [XmlEnum(":observer")]
        Observer
    }

    /// <summary>
    /// OAL abstract target type for solar system objects
    /// </summary>
    [XmlInclude(typeof(OALTargetSun))]
    [XmlInclude(typeof(OALTargetPlanet))]
    [XmlInclude(typeof(OALTargetMoon))]
    [XmlInclude(typeof(OALTargetMinorPlanet))]
    [XmlInclude(typeof(OALTargetComet))]
    [Serializable]
    [XmlType(TypeName = "SolarSystemTargetType", Namespace = OALData.OAL)]
    public abstract class OALTargetSolarSystem : OALTarget { }

    /// <summary>
    /// OAL target type for Sun
    /// </summary>
    [CelestialObjectType("Sun")]
    [Serializable]
    [XmlType(TypeName = "SunTargetType", Namespace = OALData.OAL)]
    public class OALTargetSun : OALTargetSolarSystem { }

    /// <summary>
    /// OAL target type for planets
    /// </summary>
    [CelestialObjectType("Planet")]
    [Serializable]
    [XmlType(TypeName = "PlanetTargetType", Namespace = OALData.OAL)]
    public class OALTargetPlanet : OALTargetSolarSystem { }

    /// <summary>
    /// OAL target type for Moon
    /// </summary>
    [CelestialObjectType("Moon")]
    [Serializable]
    [XmlType(TypeName = "MoonTargetType", Namespace = OALData.OAL)]
    public class OALTargetMoon : OALTargetSolarSystem { }

    /// <summary>
    /// OAL target type for minor planets
    /// </summary>
    [CelestialObjectType("Asteroid")]
    [Serializable]
    [XmlType(TypeName = "MinorPlanetTargetType", Namespace = OALData.OAL)]
    public class OALTargetMinorPlanet : OALTargetSolarSystem { }

    /// <summary>
    /// OAL target type for comets
    /// </summary>
    [CelestialObjectType("Comet")]
    [Serializable]
    [XmlType(TypeName = "CometTargetType", Namespace = OALData.OAL)]
    public class OALTargetComet : OALTargetSolarSystem { }

    /// <summary>
    /// OAL abstract target type for deep sky objects
    /// </summary>
    [XmlInclude(typeof(OALTargetDeepSkySC))]
    [XmlInclude(typeof(OALTargetDeepSkyQS))]
    [XmlInclude(typeof(OALTargetDeepSkyPN))]
    [XmlInclude(typeof(OALTargetDeepSkyOC))]
    [XmlInclude(typeof(OALTargetDeepSkyNA))]
    [XmlInclude(typeof(OALTargetDeepSkyGX))]
    [XmlInclude(typeof(OALTargetDeepSkyGN))]
    [XmlInclude(typeof(OALTargetDeepSkyGC))]
    [XmlInclude(typeof(OALTargetDeepSkyDS))]
    [XmlInclude(typeof(OALTargetDeepSkyDN))]
    [XmlInclude(typeof(OALTargetDeepSkyCG))]
    [XmlInclude(typeof(OALTargetDeepSkyAS))]
    [Serializable]
    [XmlType(TypeName = "deepSkyTargetType", Namespace = OALData.OAL)]
    public abstract class OALTargetDeepSky : OALTarget
    {
        [OALConverter(Property = nameof(DeepSkyTargetDetails.SmallDiameter), ExportConverter = typeof(ExportArcSecondsConverter), ImportConverter = typeof(ImportArcSecondsConverter))]
        [XmlElement(ElementName = "smallDiameter", Form = XmlSchemaForm.Unqualified)]
        public OALNonNegativeAngle SmallDiameter { get; set; }

        [OALConverter(Property = nameof(DeepSkyTargetDetails.LargeDiameter), ExportConverter = typeof(ExportArcSecondsConverter), ImportConverter = typeof(ImportArcSecondsConverter))]
        [XmlElement(ElementName = "largeDiameter", Form = XmlSchemaForm.Unqualified)]
        public OALNonNegativeAngle LargeDiameter { get; set; }

        [OALConverter(Property = nameof(DeepSkyTargetDetails.Magnitude), ExportConverter = typeof(ExportNullableDoubleConverter), ImportConverter = typeof(SimpleConverter))]
        [XmlElement(ElementName = "visMag", Form = XmlSchemaForm.Unqualified)]
        public double VisMag { get; set; }

        [XmlIgnore]
        public bool VisMagSpecified { get; set; }

        [OALConverter(Property = nameof(DeepSkyTargetDetails.Brightness), ExportConverter = typeof(ExportSurfBrightnessConverter), ImportConverter = typeof(ImportSurfBrightnessConverter))]
        [XmlElement(ElementName = "surfBr", Form = XmlSchemaForm.Unqualified)]
        public OALSurfaceBrightness SurfBr { get; set; }
    }

    /// <summary>
    /// OAL target type for star clouds (deep sky)
    /// </summary>
    [CelestialObjectType("DeepSky.StarCloud")]
    [Serializable]
    [XmlType(TypeName = "deepSkySC", Namespace = OALData.OAL)]
    public class OALTargetDeepSkySC : OALTargetDeepSky
    {
        /// <summary>
        /// Position angle of large axis in degrees
        /// </summary>
        [OALConverter(Property = nameof(DeepSkyStarCloudTargetDetails.PositionAngle), ExportConverter = typeof(ToStringConverter), ImportConverter = typeof(ImportNullableIntConverter))]
        [XmlElement(ElementName = "pa", Form = XmlSchemaForm.Unqualified, DataType = "integer")]
        public string PositionAngle { get; set; }
    }

    /// <summary>
    ///  OAL target type for quasars (deep sky)
    /// </summary>
    [CelestialObjectType("DeepSky.Quasar")]
    [Serializable]
    [XmlType(TypeName = "deepSkyQS", Namespace = OALData.OAL)]
    public class OALTargetDeepSkyQS : OALTargetDeepSky { }

    /// <summary>
    /// OAL target type for planetary nebulae (deep sky)
    /// </summary>
    [CelestialObjectType("DeepSky.PlanetaryNebula")]
    [Serializable]
    [XmlType(TypeName = "deepSkyPN", Namespace = OALData.OAL)]
    public class OALTargetDeepSkyPN : OALTargetDeepSky
    {
        [OALConverter(Property = nameof(DeepSkyPlanetaryNebulaTargetDetails.CentralStarMagnitude), ExportConverter = typeof(ExportNullableDoubleConverter), ImportConverter = typeof(SimpleConverter))]
        [XmlElement(ElementName = "magStar", Form = XmlSchemaForm.Unqualified)]
        public double MagStar { get; set; }

        [XmlIgnore]
        public bool MagStarSpecified { get; set; }
    }

    [CelestialObjectType("DeepSky.OpenCluster")]
    [Serializable]
    [XmlType(TypeName = "deepSkyOC", Namespace = OALData.OAL)]
    public class OALTargetDeepSkyOC : OALTargetDeepSky
    {
        /// <summary>
        /// Number of stars
        /// </summary>
        [OALConverter(Property = nameof(DeepSkyOpenClusterTargetDetails.StarsCount), ExportConverter = typeof(ToStringConverter), ImportConverter = typeof(ImportNullableIntConverter))]
        [XmlElement(ElementName = "stars", Form = XmlSchemaForm.Unqualified, DataType = "positiveInteger")]
        public string Stars { get; set; }

        /// <summary>
        /// Magnitude of brightest star in [mag] 
        /// </summary>
        [OALConverter(Property = nameof(DeepSkyOpenClusterTargetDetails.BrightestStarMagnitude), ExportConverter = typeof(ExportNullableDoubleConverter), ImportConverter = typeof(SimpleConverter))]
        [XmlElement(ElementName = "brightestStar", Form = XmlSchemaForm.Unqualified)]
        public double BrightestStar { get; set; }

        /// <summary>
        /// Flag indicating <see cref="BrightestStar"/> specified
        /// </summary>
        [XmlIgnore]
        public bool BrightestStarSpecified { get; set; }

        /// <summary>
        /// Classification according to Trumpler
        /// </summary>
        [OALConverter(Property = nameof(DeepSkyOpenClusterTargetDetails.TrumplerClass))]
        [XmlElement(ElementName = "class", Form = XmlSchemaForm.Unqualified)]
        public string TrumplerClass { get; set; }
    }

    [CelestialObjectType("DeepSky.Unspecified")]
    [Serializable]
    [XmlType(TypeName = "deepSkyNA", Namespace = OALData.OAL)]
    public class OALTargetDeepSkyNA : OALTargetDeepSky { }

    [CelestialObjectType("DeepSky.Galaxy")]
    [Serializable]
    [XmlType(TypeName = "deepSkyGX", Namespace = OALData.OAL)]
    public class OALTargetDeepSkyGX : OALTargetDeepSky
    {
        [OALConverter(Property = nameof(DeepSkyGalaxyTargetDetails.HubbleType))]
        [XmlElement(ElementName = "hubbleType", Form = XmlSchemaForm.Unqualified)]
        public string HubbleType { get; set; }

        [OALConverter(Property = nameof(DeepSkyGalaxyTargetDetails.PositionAngle), ImportConverter = typeof(ImportNullableIntConverter), ExportConverter = typeof(ToStringConverter))]
        [XmlElement(ElementName = "pa", Form = XmlSchemaForm.Unqualified, DataType = "integer")]
        public string PositionAngle { get; set; }
    }

    [CelestialObjectType("DeepSky.GalacticNebula")]
    [Serializable]
    [XmlType(TypeName = "deepSkyGN", Namespace = OALData.OAL)]
    public class OALTargetDeepSkyGN : OALTargetDeepSky
    {
        [OALConverter(Property = nameof(DeepSkyGalaxyNebulaTargetDetails.NebulaType))]
        [XmlElement(ElementName = "nebulaType", Form = XmlSchemaForm.Unqualified)]
        public string NebulaType { get; set; }

        [OALConverter(Property = nameof(DeepSkyGalaxyNebulaTargetDetails.PositionAngle), ImportConverter = typeof(ImportNullableIntConverter), ExportConverter = typeof(ToStringConverter))]
        [XmlElement(ElementName = "pa", Form = XmlSchemaForm.Unqualified, DataType = "integer")]
        public string PositionAngle { get; set; }
    }

    [CelestialObjectType("DeepSky.GlobularCluster")]
    [Serializable]
    [XmlType(TypeName = "deepSkyGC", Namespace = OALData.OAL)]
    public class OALTargetDeepSkyGC : OALTargetDeepSky
    {
        [OALConverter(Property = nameof(DeepSkyGlobularClusterTargetDetails.MagStars), ExportConverter = typeof(ExportNullableDoubleConverter), ImportConverter = typeof(SimpleConverter))]
        [XmlElement(ElementName = "magStars", Form = XmlSchemaForm.Unqualified)]
        public double MagStars { get; set; }

        [XmlIgnore]
        public bool MagStarsSpecified { get; set; }

        [OALConverter(Property = nameof(DeepSkyGlobularClusterTargetDetails.Concentration))]
        [XmlElement(ElementName = "conc", Form = XmlSchemaForm.Unqualified)]
        public string Conc { get; set; }
    }

    [CelestialObjectType("DeepSky.DoubleStar")]
    [Serializable]
    [XmlType(TypeName = "deepSkyDS", Namespace = OALData.OAL)]
    public class OALTargetDeepSkyDS : OALTargetDeepSky
    {
        [OALConverter(Property = nameof(DeepSkyDoubleStarTargetDetails.Separation), ExportConverter = typeof(ExportArcSecondsConverter), ImportConverter = typeof(ImportArcSecondsConverter))]
        [XmlElement(ElementName = "separation", Form = XmlSchemaForm.Unqualified)]
        public OALNonNegativeAngle Separation { get; set; }

        [OALConverter(Property = nameof(DeepSkyDoubleStarTargetDetails.PositionAngle), ExportConverter = typeof(ToStringConverter), ImportConverter = typeof(ImportNullableIntConverter))]
        [XmlElement(ElementName = "pa", Form = XmlSchemaForm.Unqualified, DataType = "integer")]
        public string PositionAngle { get; set; }

        [OALConverter(Property = nameof(DeepSkyDoubleStarTargetDetails.CompanionMagnitude), ExportConverter = typeof(ExportNullableDoubleConverter), ImportConverter = typeof(SimpleConverter))]
        [XmlElement(ElementName = "magComp", Form = XmlSchemaForm.Unqualified)]
        public double MagComp { get; set; }

        [XmlIgnore]
        public bool MagCompSpecified { get; set; }
    }

    [CelestialObjectType("DeepSky.DarkNebula")]
    [Serializable]
    [XmlType(TypeName = "deepSkyDN", Namespace = OALData.OAL)]
    public class OALTargetDeepSkyDN : OALTargetDeepSky
    {
        [OALConverter(Property = nameof(DeepSkyDarkNebulaTargetDetails.PositionAngle), ExportConverter = typeof(ExportPosAngleConverter), ImportConverter = typeof(ImportNullableIntConverter))]
        [XmlElement(ElementName = "pa", Form = XmlSchemaForm.Unqualified, DataType = "integer")]
        public string PositionAngle { get; set; }

        [OALConverter(Property = nameof(DeepSkyDarkNebulaTargetDetails.Opacity), ExportConverter = typeof(ToStringConverter), ImportConverter = typeof(ImportNullableIntConverter))]
        [XmlElement(ElementName = "opacity", Form = XmlSchemaForm.Unqualified, DataType = "integer")]
        public string Opacity { get; set; }
    }

    [CelestialObjectType("DeepSky.GalaxyCluster")]
    [Serializable]
    [XmlType(TypeName = "deepSkyCG", Namespace = OALData.OAL)]
    public class OALTargetDeepSkyCG : OALTargetDeepSky
    {
        [OALConverter(Property = nameof(DeepSkyClusterOfGalaxiesTargetDetails.Mag10), ExportConverter = typeof(ExportNullableDoubleConverter), ImportConverter = typeof(SimpleConverter))]
        [XmlElement(ElementName = "mag10", Form = XmlSchemaForm.Unqualified)]
        public double Mag10 { get; set; }

        [XmlIgnore]
        public bool Mag10Specified { get; set; }
    }

    [CelestialObjectType("DeepSky.Asterism")]
    [Serializable]
    [XmlType(TypeName = "deepSkyAS", Namespace = OALData.OAL)]
    public class OALTargetDeepSkyAS : OALTargetDeepSky
    {
        [OALConverter(Property = nameof(DeepSkyAsterismTargetDetails.PositionAngle), ExportConverter = typeof(ExportPosAngleConverter), ImportConverter = typeof(ImportNullableIntConverter))]
        [XmlElement(ElementName = "pa", Form = XmlSchemaForm.Unqualified, DataType = "integer")]
        public string PositionAngle { get; set; }
    }

    [CelestialObjectType("DeepSky.MultipleStar")]
    [Serializable]
    [XmlType(TypeName = "deepSkyMS", Namespace = OALData.OAL)]
    public class OALTargetDeepSkyMS : OALTarget
    {
        [OALConverter(Property = nameof(DeepSkyMultipleStarTargetDetails.Component))]
        [XmlElement(ElementName = "component", Form = XmlSchemaForm.Unqualified, DataType = "IDREF")]
        public string[] Component { get; set; }
    }

    [CelestialObjectType("Star")]
    [XmlInclude(typeof(OALTargetVariableStar))]
    [Serializable]
    [XmlType(TypeName = "starTargetType", Namespace = OALData.OAL)]
    public class OALTargetStar : OALTarget
    {
        [OALConverter(Property = nameof(StarTargetDetails.Magnitude), ExportConverter = typeof(ExportNullableDoubleConverter), ImportConverter = typeof(SimpleConverter))]
        [XmlElement(ElementName = "apparentMag", Form = XmlSchemaForm.Unqualified)]
        public double ApparentMag { get; set; }

        [XmlIgnore]
        public bool ApparentMagSpecified { get; set; }

        [OALConverter(Property = nameof(StarTargetDetails.Classification))]
        [XmlElement(ElementName = "classification", Form = XmlSchemaForm.Unqualified)]
        public string Classification { get; set; }
    }

    [CelestialObjectTypeDiscriminator(typeof(OALTargetVariableStarDicriminator))]
    [CelestialObjectType("VarStar")]
    [CelestialObjectType("Nova")]
    [Serializable]
    [XmlType(TypeName = "variableStarTargetType", Namespace = OALData.OAL)]
    public class OALTargetVariableStar : OALTargetStar
    {
        [OALConverter(Property = nameof(VariableStarTargetDetails.VarStarType))]
        [XmlElement(ElementName = "type", Form = XmlSchemaForm.Unqualified)]
        public string Type { get; set; }

        [OALConverter(Property = nameof(VariableStarTargetDetails.MaxMagnitude), ExportConverter = typeof(ExportNullableDoubleConverter), ImportConverter = typeof(SimpleConverter))]
        [XmlElement(ElementName = "maxApparentMag", Form = XmlSchemaForm.Unqualified)]
        public double MaxApparentMag { get; set; }

        [XmlIgnore]
        public bool MaxApparentMagSpecified { get; set; }

        [OALConverter(Property = nameof(VariableStarTargetDetails.Period), ExportConverter = typeof(ExportNullableDoubleConverter), ImportConverter = typeof(SimpleConverter))]
        [XmlElement(ElementName = "period", Form = XmlSchemaForm.Unqualified)]
        public double Period { get; set; }

        [XmlIgnore]
        public bool PeriodSpecified { get; set; }
    }

    /// <summary>
    /// Defines common remarks or conditions of observations conducted during one night/session
    /// </summary>
    [Serializable]
    [XmlType(TypeName = "sessionType", Namespace = OALData.OAL)]
    public class OALSession
    {
        /// <summary>
        /// Start of observation session
        /// </summary>
        [XmlElement(ElementName = "begin", Form = XmlSchemaForm.Unqualified)]
        public string Begin { get; set; }

        /// <summary>
        /// End of observation session
        /// </summary>
        [XmlElement(ElementName = "end", Form = XmlSchemaForm.Unqualified)]
        public string End { get; set; }

        /// <summary>
        /// Site where session took place
        /// </summary>
        [XmlElement(ElementName = "site", Form = XmlSchemaForm.Unqualified, DataType = "IDREF")]
        public string SiteId { get; set; }

        /// <summary>
        /// Who participated in the observation session?
        /// </summary>
        [XmlElement(ElementName = "coObserver", Form = XmlSchemaForm.Unqualified, DataType = "IDREF")]
        public string[] CoObservers { get; set; }

        /// <summary>
        /// Comments about the weather situation
        /// </summary>
        [XmlElement(ElementName = "weather", Form = XmlSchemaForm.Unqualified)]
        public string Weather { get; set; }

        /// <summary>
        /// Comments on the (optical or electronical) equipment used
        /// </summary>
        [XmlElement(ElementName = "equipment", Form = XmlSchemaForm.Unqualified)]
        public string Equipment { get; set; }

        /// <summary>
        /// Any other comments
        /// </summary>
        [XmlElement(ElementName = "comments", Form = XmlSchemaForm.Unqualified)]
        public string Comments { get; set; }

        /// <summary>
        /// Image files related to the session
        /// </summary>
        [XmlElement(ElementName = "image", Form = XmlSchemaForm.Unqualified)]
        public string[] Images { get; set; }

        /// <summary>
        /// Identifier of the session
        /// </summary>
        [XmlAttribute(AttributeName = "id", DataType = "ID")]
        public string Id { get; set; }

        /// <summary>
        /// Optional language code.
        /// The "lang" attribute carries an ISO two letter language identifier. 
		/// It can be used to indicate the language of the description 
		/// By the help of this attribute, applications may filter observations 
		/// based on user preferences.
        /// </summary>
        [XmlAttribute(AttributeName = "lang")]
        public string Lang { get; set; }
    }

    /// <summary>
    /// Defines observation site 
    /// </summary>
    [Serializable]
    [XmlType(TypeName = "siteType", Namespace = OALData.OAL)]
    public class OALSite
    {

        [XmlElement(ElementName = "name", Form = XmlSchemaForm.Unqualified)]
        public string Name { get; set; }

        /// <summary>
        /// Geographical longitude; eastwards positive
        /// </summary>
        [XmlElement(ElementName = "longitude", Form = XmlSchemaForm.Unqualified)]
        public OALAngle Longitude { get; set; }

        /// <summary>
        /// Geographical latitude; northwards positive
        /// </summary>
        [XmlElement(ElementName = "latitude", Form = XmlSchemaForm.Unqualified)]
        public OALAngle Latitude { get; set; }

        /// <summary>
        /// Elevation above sea level, in meters
        /// </summary>
        [XmlElement(ElementName = "elevation", Form = XmlSchemaForm.Unqualified)]
        public double Elevation { get; set; }


        [XmlIgnore]
        public bool ElevationSpecified { get; set; }

        /// <summary>
        /// Offset from UT in [min] not including daylight savings time, West of Greenwich is negative and East is positive
        /// </summary>
        [XmlElement(ElementName = "timezone", Form = XmlSchemaForm.Unqualified, DataType = "integer")]
        public string TimeZone { get; set; } = "0";

        /// <summary>
        /// IAU Code for site
        /// </summary>
        [XmlElement(ElementName = "code", Form = XmlSchemaForm.Unqualified, DataType = "integer")]
        public string Code { get; set; }


        [XmlAttribute(AttributeName = "id", DataType = "ID")]
        public string Id { get; set; }
    }
}
