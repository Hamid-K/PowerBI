using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000CDC RID: 3292
	internal abstract class CubeContextProvider
	{
		// Token: 0x17001AB7 RID: 6839
		// (get) Token: 0x06005952 RID: 22866
		public abstract IResource Resource { get; }

		// Token: 0x17001AB8 RID: 6840
		// (get) Token: 0x06005953 RID: 22867
		public abstract IEngineHost EngineHost { get; }

		// Token: 0x06005954 RID: 22868
		public abstract bool TryCreateContext(QueryCubeExpression expression, IList<ParameterArguments> parameters, out CubeContext context);

		// Token: 0x06005955 RID: 22869
		public abstract CubeObjectKind GetObjectKind(IdentifierCubeExpression identifier);

		// Token: 0x06005956 RID: 22870
		public abstract string GetDisplayName(IdentifierCubeExpression identifier);

		// Token: 0x06005957 RID: 22871
		public abstract TypeValue GetType(IdentifierCubeExpression identifier);

		// Token: 0x06005958 RID: 22872
		public abstract IdentifierCubeExpression GetProperty(IdentifierCubeExpression dimensionAttribute, CubePropertyKind kind, string userDefinedIdentifier = null);

		// Token: 0x06005959 RID: 22873
		public abstract IdentifierCubeExpression GetPropertyDimensionAttribute(IdentifierCubeExpression property);

		// Token: 0x0600595A RID: 22874
		public abstract CubePropertyKind GetPropertyKind(IdentifierCubeExpression property);

		// Token: 0x0600595B RID: 22875 RVA: 0x0007D355 File Offset: 0x0007B555
		public virtual bool TryGetPropertyKey(IdentifierCubeExpression propertyExpression, out IdentifierCubeExpression keyExpression)
		{
			keyExpression = null;
			return false;
		}

		// Token: 0x0600595C RID: 22876 RVA: 0x00002105 File Offset: 0x00000305
		public virtual bool PropertyIsKey(IdentifierCubeExpression propertyExpression)
		{
			return false;
		}

		// Token: 0x0600595D RID: 22877
		public abstract IdentifierCubeExpression GetMeasureProperty(IdentifierCubeExpression dimensionAttribute, string userDefinedIdentifier = null);

		// Token: 0x0600595E RID: 22878 RVA: 0x00002105 File Offset: 0x00000305
		public virtual bool IsDimensionAttributeUniqueId(IdentifierCubeExpression dimensionAttribute)
		{
			return false;
		}

		// Token: 0x0600595F RID: 22879 RVA: 0x0007D355 File Offset: 0x0007B555
		public virtual bool TryGetDynamicMeasure(CubeExpression expression, out IdentifierCubeExpression measure)
		{
			measure = null;
			return false;
		}

		// Token: 0x06005960 RID: 22880 RVA: 0x000912D6 File Offset: 0x0008F4D6
		public virtual bool TryGetDynamicDimensionAttribute(CubeExpression expression, TypeValue typeValue, out IdentifierCubeExpression dimensionAttribute)
		{
			dimensionAttribute = null;
			return false;
		}
	}
}
