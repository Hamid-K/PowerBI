using System;
using System.Xml;
using Microsoft.Data.OData.JsonLight;

namespace Microsoft.Data.OData
{
	// Token: 0x02000132 RID: 306
	public sealed class ODataInstanceAnnotation : ODataAnnotatable
	{
		// Token: 0x060007E0 RID: 2016 RVA: 0x00019FE2 File Offset: 0x000181E2
		public ODataInstanceAnnotation(string name, ODataValue value)
		{
			ODataInstanceAnnotation.ValidateName(name);
			ODataInstanceAnnotation.ValidateValue(value);
			this.Name = name;
			this.Value = value;
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060007E1 RID: 2017 RVA: 0x0001A004 File Offset: 0x00018204
		// (set) Token: 0x060007E2 RID: 2018 RVA: 0x0001A00C File Offset: 0x0001820C
		public string Name { get; private set; }

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060007E3 RID: 2019 RVA: 0x0001A015 File Offset: 0x00018215
		// (set) Token: 0x060007E4 RID: 2020 RVA: 0x0001A01D File Offset: 0x0001821D
		public ODataValue Value { get; private set; }

		// Token: 0x060007E5 RID: 2021 RVA: 0x0001A028 File Offset: 0x00018228
		internal static void ValidateName(string name)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(name, "name");
			if (name.IndexOf('.') < 0 || name.get_Chars(0) == '.' || name.get_Chars(name.Length - 1) == '.')
			{
				throw new ArgumentException(Strings.ODataInstanceAnnotation_NeedPeriodInName(name));
			}
			if (ODataAnnotationNames.IsODataAnnotationName(name))
			{
				throw new ArgumentException(Strings.ODataInstanceAnnotation_ReservedNamesNotAllowed(name, "odata."));
			}
			try
			{
				XmlConvert.VerifyNCName(name);
			}
			catch (XmlException ex)
			{
				throw new ArgumentException(Strings.ODataInstanceAnnotation_BadTermName(name), ex);
			}
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x0001A0B4 File Offset: 0x000182B4
		internal static void ValidateValue(ODataValue value)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataValue>(value, "value");
			if (value is ODataStreamReferenceValue)
			{
				throw new ArgumentException(Strings.ODataInstanceAnnotation_ValueCannotBeODataStreamReferenceValue, "value");
			}
		}
	}
}
