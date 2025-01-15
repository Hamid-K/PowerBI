using System;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000087 RID: 135
	internal sealed class ExpressionContext
	{
		// Token: 0x06000549 RID: 1353 RVA: 0x0001372E File Offset: 0x0001192E
		internal ExpressionContext(string owningQueryName, SemanticQueryObjectType objectType, object objectId)
		{
			this.OwningQueryName = owningQueryName;
			this.ObjectType = objectType;
			this.ObjectId = objectId;
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x0001374B File Offset: 0x0001194B
		internal string OwningQueryName { get; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x00013753 File Offset: 0x00011953
		internal SemanticQueryObjectType ObjectType { get; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x0001375B File Offset: 0x0001195B
		internal object ObjectId { get; }
	}
}
