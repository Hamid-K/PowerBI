using System;
using System.Xml;
using Microsoft.OData.Core.JsonLight;

namespace Microsoft.OData.Core
{
	// Token: 0x0200017B RID: 379
	public sealed class ODataInstanceAnnotation : ODataAnnotatable
	{
		// Token: 0x06000DE0 RID: 3552 RVA: 0x00031A25 File Offset: 0x0002FC25
		public ODataInstanceAnnotation(string name, ODataValue value)
		{
			ODataInstanceAnnotation.ValidateName(name);
			ODataInstanceAnnotation.ValidateValue(value);
			this.Name = name;
			this.Value = value;
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000DE1 RID: 3553 RVA: 0x00031A47 File Offset: 0x0002FC47
		// (set) Token: 0x06000DE2 RID: 3554 RVA: 0x00031A4F File Offset: 0x0002FC4F
		public string Name { get; private set; }

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000DE3 RID: 3555 RVA: 0x00031A58 File Offset: 0x0002FC58
		// (set) Token: 0x06000DE4 RID: 3556 RVA: 0x00031A60 File Offset: 0x0002FC60
		public ODataValue Value { get; private set; }

		// Token: 0x06000DE5 RID: 3557 RVA: 0x00031A6C File Offset: 0x0002FC6C
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

		// Token: 0x06000DE6 RID: 3558 RVA: 0x00031AF8 File Offset: 0x0002FCF8
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
