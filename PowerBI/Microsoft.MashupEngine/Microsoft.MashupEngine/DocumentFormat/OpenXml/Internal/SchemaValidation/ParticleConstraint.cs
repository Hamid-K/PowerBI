using System;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02003111 RID: 12561
	internal abstract class ParticleConstraint
	{
		// Token: 0x0601B3D4 RID: 111572 RVA: 0x000020FD File Offset: 0x000002FD
		internal ParticleConstraint()
		{
		}

		// Token: 0x170098C2 RID: 39106
		// (get) Token: 0x0601B3D5 RID: 111573 RVA: 0x003736D0 File Offset: 0x003718D0
		// (set) Token: 0x0601B3D6 RID: 111574 RVA: 0x0000EE09 File Offset: 0x0000D009
		internal virtual ParticleType ParticleType
		{
			get
			{
				return ParticleType.Invalid;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x170098C3 RID: 39107
		// (get) Token: 0x0601B3D7 RID: 111575 RVA: 0x003736D7 File Offset: 0x003718D7
		// (set) Token: 0x0601B3D8 RID: 111576 RVA: 0x003736DF File Offset: 0x003718DF
		internal int MinOccurs { get; set; }

		// Token: 0x170098C4 RID: 39108
		// (get) Token: 0x0601B3D9 RID: 111577 RVA: 0x003736E8 File Offset: 0x003718E8
		// (set) Token: 0x0601B3DA RID: 111578 RVA: 0x003736F0 File Offset: 0x003718F0
		internal int MaxOccurs { get; set; }

		// Token: 0x170098C5 RID: 39109
		// (get) Token: 0x0601B3DB RID: 111579 RVA: 0x003736F9 File Offset: 0x003718F9
		internal bool UnboundedMaxOccurs
		{
			get
			{
				return this.MaxOccurs == 0;
			}
		}

		// Token: 0x170098C6 RID: 39110
		// (get) Token: 0x0601B3DC RID: 111580 RVA: 0x00373704 File Offset: 0x00371904
		internal bool CanOccursMoreThanOne
		{
			get
			{
				return this.UnboundedMaxOccurs || this.MaxOccurs > 1;
			}
		}

		// Token: 0x0601B3DD RID: 111581 RVA: 0x00373719 File Offset: 0x00371919
		internal bool MaxOccursGreaterThan(int count)
		{
			return this.UnboundedMaxOccurs || this.MaxOccurs > count;
		}

		// Token: 0x170098C7 RID: 39111
		// (get) Token: 0x0601B3DE RID: 111582 RVA: 0x0037372E File Offset: 0x0037192E
		// (set) Token: 0x0601B3DF RID: 111583 RVA: 0x0000336E File Offset: 0x0000156E
		internal virtual int ElementId
		{
			get
			{
				return 65535;
			}
			set
			{
			}
		}

		// Token: 0x170098C8 RID: 39112
		// (get) Token: 0x0601B3E0 RID: 111584 RVA: 0x000020FA File Offset: 0x000002FA
		// (set) Token: 0x0601B3E1 RID: 111585 RVA: 0x0000EE09 File Offset: 0x0000D009
		internal virtual ParticleConstraint[] ChildrenParticles
		{
			get
			{
				return null;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x170098C9 RID: 39113
		// (get) Token: 0x0601B3E2 RID: 111586 RVA: 0x000020FA File Offset: 0x000002FA
		internal virtual IParticleValidator ParticleValidator
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0601B3E3 RID: 111587 RVA: 0x00373738 File Offset: 0x00371938
		internal bool IsSimple()
		{
			bool flag = true;
			if (this.ChildrenParticles != null)
			{
				foreach (ParticleConstraint particleConstraint in this.ChildrenParticles)
				{
					if (particleConstraint.ParticleType == ParticleType.All || particleConstraint.ParticleType == ParticleType.Choice || particleConstraint.ParticleType == ParticleType.Group || particleConstraint.ParticleType == ParticleType.Sequence || particleConstraint.ParticleType == ParticleType.Any || particleConstraint.ParticleType == ParticleType.AnyWithUri)
					{
						flag = false;
					}
				}
			}
			return flag;
		}

		// Token: 0x0601B3E4 RID: 111588 RVA: 0x003737A4 File Offset: 0x003719A4
		internal static ParticleConstraint CreateParticleConstraint(ParticleType particleType)
		{
			switch (particleType)
			{
			case ParticleType.Element:
				return new ElementParticle();
			case ParticleType.All:
				break;
			case ParticleType.Any:
				return new AnyParticle();
			default:
				if (particleType == ParticleType.AnyWithUri)
				{
					return new NsAnyParticle();
				}
				if (particleType == ParticleType.Invalid)
				{
					return null;
				}
				break;
			}
			return new CompositeParticle();
		}
	}
}
