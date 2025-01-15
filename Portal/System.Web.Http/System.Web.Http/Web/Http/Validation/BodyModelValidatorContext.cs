using System;
using System.Collections.Generic;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using System.Web.Http.ModelBinding;

namespace System.Web.Http.Validation
{
	// Token: 0x0200008A RID: 138
	public class BodyModelValidatorContext
	{
		// Token: 0x06000365 RID: 869 RVA: 0x00009F3C File Offset: 0x0000813C
		public BodyModelValidatorContext(ModelStateDictionary modelState)
		{
			if (modelState == null)
			{
				throw new ArgumentNullException("modelState");
			}
			this.KeyBuilders = new Stack<IBodyModelValidatorKeyBuilder>();
			this.ModelState = modelState;
			this.Visited = new HashSet<object>(ReferenceEqualityComparer.Instance);
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000366 RID: 870 RVA: 0x00009F74 File Offset: 0x00008174
		// (set) Token: 0x06000367 RID: 871 RVA: 0x00009F7C File Offset: 0x0000817C
		public ModelMetadataProvider MetadataProvider { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000368 RID: 872 RVA: 0x00009F85 File Offset: 0x00008185
		// (set) Token: 0x06000369 RID: 873 RVA: 0x00009F8D File Offset: 0x0000818D
		public HttpActionContext ActionContext { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600036A RID: 874 RVA: 0x00009F96 File Offset: 0x00008196
		// (set) Token: 0x0600036B RID: 875 RVA: 0x00009F9E File Offset: 0x0000819E
		public IModelValidatorCache ValidatorCache { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600036C RID: 876 RVA: 0x00009FA7 File Offset: 0x000081A7
		// (set) Token: 0x0600036D RID: 877 RVA: 0x00009FAF File Offset: 0x000081AF
		public ModelStateDictionary ModelState { get; private set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600036E RID: 878 RVA: 0x00009FB8 File Offset: 0x000081B8
		// (set) Token: 0x0600036F RID: 879 RVA: 0x00009FC0 File Offset: 0x000081C0
		public HashSet<object> Visited { get; private set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000370 RID: 880 RVA: 0x00009FC9 File Offset: 0x000081C9
		// (set) Token: 0x06000371 RID: 881 RVA: 0x00009FD1 File Offset: 0x000081D1
		public Stack<IBodyModelValidatorKeyBuilder> KeyBuilders { get; private set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000372 RID: 882 RVA: 0x00009FDA File Offset: 0x000081DA
		// (set) Token: 0x06000373 RID: 883 RVA: 0x00009FE2 File Offset: 0x000081E2
		public string RootPrefix { get; set; }
	}
}
