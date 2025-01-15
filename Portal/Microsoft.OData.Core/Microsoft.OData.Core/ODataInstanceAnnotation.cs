using System;
using System.Xml;
using Microsoft.OData.JsonLight;

namespace Microsoft.OData
{
	// Token: 0x02000090 RID: 144
	public sealed class ODataInstanceAnnotation : ODataAnnotatable
	{
		// Token: 0x06000531 RID: 1329 RVA: 0x0000CA1C File Offset: 0x0000AC1C
		public ODataInstanceAnnotation(string name, ODataValue value)
			: this(name, value, false)
		{
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0000CA28 File Offset: 0x0000AC28
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

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x0000CA7C File Offset: 0x0000AC7C
		// (set) Token: 0x06000534 RID: 1332 RVA: 0x0000CA84 File Offset: 0x0000AC84
		public string Name { get; private set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x0000CA8D File Offset: 0x0000AC8D
		// (set) Token: 0x06000536 RID: 1334 RVA: 0x0000CA95 File Offset: 0x0000AC95
		public ODataValue Value { get; private set; }

		// Token: 0x06000537 RID: 1335 RVA: 0x0000CAA0 File Offset: 0x0000ACA0
		internal static void ValidateName(string name)
		{
			if (name.IndexOf('.') < 0 || name[0] == '.' || name[name.Length - 1] == '.')
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

		// Token: 0x06000538 RID: 1336 RVA: 0x0000CB0C File Offset: 0x0000AD0C
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
