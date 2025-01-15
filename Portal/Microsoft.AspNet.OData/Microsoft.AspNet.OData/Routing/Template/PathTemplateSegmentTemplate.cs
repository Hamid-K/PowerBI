using System;
using System.Collections.Generic;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNet.OData.Routing.Conventions;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Routing.Template
{
	// Token: 0x02000084 RID: 132
	public class PathTemplateSegmentTemplate : ODataPathSegmentTemplate
	{
		// Token: 0x060004CE RID: 1230 RVA: 0x0000FD2C File Offset: 0x0000DF2C
		public PathTemplateSegmentTemplate(PathTemplateSegment segment)
		{
			if (segment == null)
			{
				throw Error.ArgumentNull("segment");
			}
			this.TemplateSegment = segment;
			string text;
			this.SegmentName = segment.TranslatePathTemplateSegment(out text);
			this.PropertyName = text;
			this.TreatPropertyNameAsParameterName = false;
			if (RoutingConventionHelpers.IsRouteParameter(this.PropertyName))
			{
				this.PropertyName = this.PropertyName.Substring(1, this.PropertyName.Length - 2);
				this.TreatPropertyNameAsParameterName = true;
				if (string.IsNullOrEmpty(this.PropertyName))
				{
					Error.Format(SRResources.EmptyParameterAlias, new object[] { this.PropertyName, segment.LiteralText });
				}
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x0000FDD3 File Offset: 0x0000DFD3
		// (set) Token: 0x060004D0 RID: 1232 RVA: 0x0000FDDB File Offset: 0x0000DFDB
		public string PropertyName { get; private set; }

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x0000FDE4 File Offset: 0x0000DFE4
		// (set) Token: 0x060004D2 RID: 1234 RVA: 0x0000FDEC File Offset: 0x0000DFEC
		public string SegmentName { get; private set; }

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x0000FDF5 File Offset: 0x0000DFF5
		// (set) Token: 0x060004D4 RID: 1236 RVA: 0x0000FDFD File Offset: 0x0000DFFD
		private bool TreatPropertyNameAsParameterName { get; set; }

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x0000FE06 File Offset: 0x0000E006
		// (set) Token: 0x060004D6 RID: 1238 RVA: 0x0000FE0E File Offset: 0x0000E00E
		public PathTemplateSegment TemplateSegment { get; private set; }

		// Token: 0x060004D7 RID: 1239 RVA: 0x0000FE18 File Offset: 0x0000E018
		public override bool TryMatch(ODataPathSegment pathSegment, IDictionary<string, object> values)
		{
			DynamicPathSegment dynamicPathSegment = pathSegment as DynamicPathSegment;
			if (dynamicPathSegment == null)
			{
				return false;
			}
			if (this.TreatPropertyNameAsParameterName)
			{
				values[this.PropertyName] = dynamicPathSegment.Identifier;
				values["DF908045-6922-46A0-82F2-2F6E7F43D1B1_" + this.PropertyName] = new ODataParameterValue(dynamicPathSegment.Identifier, EdmLibHelpers.GetEdmPrimitiveTypeReferenceOrNull(typeof(string)));
				return true;
			}
			return this.PropertyName == dynamicPathSegment.Identifier;
		}
	}
}
