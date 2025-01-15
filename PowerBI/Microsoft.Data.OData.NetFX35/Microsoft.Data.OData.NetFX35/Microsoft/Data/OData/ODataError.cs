using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Microsoft.Data.OData
{
	// Token: 0x0200028D RID: 653
	[DebuggerDisplay("{ErrorCode}: {Message}")]
	[Serializable]
	public sealed class ODataError : ODataAnnotatable
	{
		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x060014C2 RID: 5314 RVA: 0x0004C47B File Offset: 0x0004A67B
		// (set) Token: 0x060014C3 RID: 5315 RVA: 0x0004C483 File Offset: 0x0004A683
		public string ErrorCode { get; set; }

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x060014C4 RID: 5316 RVA: 0x0004C48C File Offset: 0x0004A68C
		// (set) Token: 0x060014C5 RID: 5317 RVA: 0x0004C494 File Offset: 0x0004A694
		public string Message { get; set; }

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x060014C6 RID: 5318 RVA: 0x0004C49D File Offset: 0x0004A69D
		// (set) Token: 0x060014C7 RID: 5319 RVA: 0x0004C4A5 File Offset: 0x0004A6A5
		public string MessageLanguage { get; set; }

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x060014C8 RID: 5320 RVA: 0x0004C4AE File Offset: 0x0004A6AE
		// (set) Token: 0x060014C9 RID: 5321 RVA: 0x0004C4B6 File Offset: 0x0004A6B6
		public ODataInnerError InnerError { get; set; }

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x060014CA RID: 5322 RVA: 0x0004C4BF File Offset: 0x0004A6BF
		// (set) Token: 0x060014CB RID: 5323 RVA: 0x0004C4C7 File Offset: 0x0004A6C7
		public ICollection<ODataInstanceAnnotation> InstanceAnnotations
		{
			get
			{
				return base.GetInstanceAnnotations();
			}
			set
			{
				base.SetInstanceAnnotations(value);
			}
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x0004C4D0 File Offset: 0x0004A6D0
		internal override void VerifySetAnnotation(object annotation)
		{
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x0004C4E8 File Offset: 0x0004A6E8
		internal IEnumerable<ODataInstanceAnnotation> GetInstanceAnnotationsForWriting()
		{
			if (this.InstanceAnnotations.Count > 0)
			{
				return this.InstanceAnnotations;
			}
			InstanceAnnotationCollection annotation = base.GetAnnotation<InstanceAnnotationCollection>();
			if (annotation != null && annotation.Count > 0)
			{
				return Enumerable.Select<KeyValuePair<string, ODataValue>, ODataInstanceAnnotation>(annotation, (KeyValuePair<string, ODataValue> ia) => new ODataInstanceAnnotation(ia.Key, ia.Value));
			}
			return Enumerable.Empty<ODataInstanceAnnotation>();
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x0004C548 File Offset: 0x0004A748
		internal void AddInstanceAnnotationForReading(string instanceAnnotationName, object instanceAnnotationValue)
		{
			ODataValue odataValue = instanceAnnotationValue.ToODataValue();
			base.GetOrCreateAnnotation<InstanceAnnotationCollection>().Add(instanceAnnotationName, odataValue);
			this.InstanceAnnotations.Add(new ODataInstanceAnnotation(instanceAnnotationName, odataValue));
		}
	}
}
