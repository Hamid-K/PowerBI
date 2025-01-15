using System;
using System.Diagnostics;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02003114 RID: 12564
	[DebuggerDisplay("NamespaceId={NamespaceId}")]
	internal class NsAnyParticle : ParticleConstraint
	{
		// Token: 0x0601B3F7 RID: 111607 RVA: 0x0037399A File Offset: 0x00371B9A
		internal NsAnyParticle()
		{
			this._particleValidator = new NsAnyParticleValidator(this);
		}

		// Token: 0x170098D1 RID: 39121
		// (get) Token: 0x0601B3F8 RID: 111608 RVA: 0x00002461 File Offset: 0x00000661
		// (set) Token: 0x0601B3F9 RID: 111609 RVA: 0x0000336E File Offset: 0x0000156E
		internal override ParticleType ParticleType
		{
			get
			{
				return ParticleType.AnyWithUri;
			}
			set
			{
			}
		}

		// Token: 0x170098D2 RID: 39122
		// (set) Token: 0x0601B3FA RID: 111610 RVA: 0x003739AE File Offset: 0x00371BAE
		internal override int ElementId
		{
			set
			{
				this._namespaceId = (byte)value;
			}
		}

		// Token: 0x170098D3 RID: 39123
		// (get) Token: 0x0601B3FB RID: 111611 RVA: 0x003739B8 File Offset: 0x00371BB8
		internal byte NamespaceId
		{
			get
			{
				return this._namespaceId;
			}
		}

		// Token: 0x170098D4 RID: 39124
		// (get) Token: 0x0601B3FC RID: 111612 RVA: 0x003739C0 File Offset: 0x00371BC0
		internal override IParticleValidator ParticleValidator
		{
			get
			{
				return this._particleValidator;
			}
		}

		// Token: 0x0400B4B6 RID: 46262
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private byte _namespaceId;

		// Token: 0x0400B4B7 RID: 46263
		private IParticleValidator _particleValidator;
	}
}
