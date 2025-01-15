using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000162 RID: 354
	[Serializable]
	internal sealed class PhaseOneConstraintException : Exception
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06002110 RID: 8464 RVA: 0x0015C5AA File Offset: 0x0015A7AA
		public ConstraintDefinition Constraint
		{
			get
			{
				return this._constraint;
			}
		}

		// Token: 0x06002111 RID: 8465 RVA: 0x0015C5B2 File Offset: 0x0015A7B2
		public PhaseOneConstraintException(ConstraintDefinition constraint)
		{
			this._constraint = constraint;
		}

		// Token: 0x06002112 RID: 8466 RVA: 0x0015C5C1 File Offset: 0x0015A7C1
		private PhaseOneConstraintException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x040018AB RID: 6315
		private ConstraintDefinition _constraint;
	}
}
