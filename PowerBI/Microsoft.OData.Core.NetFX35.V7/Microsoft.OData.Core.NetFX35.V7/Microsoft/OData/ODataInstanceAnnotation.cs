using System;
using System.Xml;
using Microsoft.OData.JsonLight;

namespace Microsoft.OData
{
	// Token: 0x0200006A RID: 106
	public sealed class ODataInstanceAnnotation : ODataAnnotatable
	{
		// Token: 0x06000375 RID: 885 RVA: 0x0000A676 File Offset: 0x00008876
		public ODataInstanceAnnotation(string name, ODataValue value)
			: this(name, value, false)
		{
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000A684 File Offset: 0x00008884
		internal ODataInstanceAnnotation(string annotationName, ODataValue annotationValue, bool isCustomAnnotation)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(annotationName, "annotationName");
			if (!isCustomAnnotation && ODataAnnotationNames.IsODataAnnotationName(annotationName))
			{
				throw new ArgumentException(Strings.ODataInstanceAnnotation_ReservedNamesNotAllowed(annotationName, "odata."));
			}
			ODataInstanceAnnotation.ValidateName(annotationName);
			ODataInstanceAnnotation.ValidateValue(annotationValue);
			this.Name = annotationName;
			this.Value = annotationValue;
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000377 RID: 887 RVA: 0x0000A6D8 File Offset: 0x000088D8
		// (set) Token: 0x06000378 RID: 888 RVA: 0x0000A6E0 File Offset: 0x000088E0
		public string Name { get; private set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000379 RID: 889 RVA: 0x0000A6E9 File Offset: 0x000088E9
		// (set) Token: 0x0600037A RID: 890 RVA: 0x0000A6F1 File Offset: 0x000088F1
		public ODataValue Value { get; private set; }

		// Token: 0x0600037B RID: 891 RVA: 0x0000A6FC File Offset: 0x000088FC
		internal static void ValidateName(string name)
		{
			if (name.IndexOf('.') < 0 || name.get_Chars(0) == '.' || name.get_Chars(name.Length - 1) == '.')
			{
				throw new ArgumentException(Strings.ODataInstanceAnnotation_NeedPeriodInName(name));
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

		// Token: 0x0600037C RID: 892 RVA: 0x0000A768 File Offset: 0x00008968
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
