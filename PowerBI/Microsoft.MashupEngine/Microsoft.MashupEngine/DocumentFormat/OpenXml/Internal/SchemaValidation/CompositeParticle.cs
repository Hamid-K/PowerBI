using System;
using System.Diagnostics;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02003115 RID: 12565
	[DebuggerDisplay("ParticleType={ParticleType}")]
	internal class CompositeParticle : ParticleConstraint
	{
		// Token: 0x0601B3FD RID: 111613 RVA: 0x003737EC File Offset: 0x003719EC
		internal CompositeParticle()
		{
		}

		// Token: 0x170098D5 RID: 39125
		// (get) Token: 0x0601B3FE RID: 111614 RVA: 0x003739C8 File Offset: 0x00371BC8
		// (set) Token: 0x0601B3FF RID: 111615 RVA: 0x003739D0 File Offset: 0x00371BD0
		internal override ParticleType ParticleType
		{
			get
			{
				return this._particleType;
			}
			set
			{
				this._particleType = value;
			}
		}

		// Token: 0x170098D6 RID: 39126
		// (get) Token: 0x0601B400 RID: 111616 RVA: 0x003739D9 File Offset: 0x00371BD9
		// (set) Token: 0x0601B401 RID: 111617 RVA: 0x003739E1 File Offset: 0x00371BE1
		internal override ParticleConstraint[] ChildrenParticles
		{
			get
			{
				return this._childrenParticles;
			}
			set
			{
				this._childrenParticles = value;
			}
		}

		// Token: 0x170098D7 RID: 39127
		// (get) Token: 0x0601B402 RID: 111618 RVA: 0x003739EA File Offset: 0x00371BEA
		internal override IParticleValidator ParticleValidator
		{
			get
			{
				if (this._particleValidator == null)
				{
					this._particleValidator = DocumentFormat.OpenXml.Internal.SchemaValidation.ParticleValidator.CreateParticleValidator(this);
				}
				return this._particleValidator;
			}
		}

		// Token: 0x0400B4B8 RID: 46264
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ParticleType _particleType;

		// Token: 0x0400B4B9 RID: 46265
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ParticleConstraint[] _childrenParticles;

		// Token: 0x0400B4BA RID: 46266
		private IParticleValidator _particleValidator;
	}
}
