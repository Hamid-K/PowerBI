using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;

namespace System.Data.Entity.Core.Mapping.Update.Internal
{
	// Token: 0x020005C2 RID: 1474
	internal class CompositeKey
	{
		// Token: 0x0600474E RID: 18254 RVA: 0x000FBF3E File Offset: 0x000FA13E
		internal CompositeKey(PropagatorResult[] constants)
		{
			this.KeyComponents = constants;
		}

		// Token: 0x0600474F RID: 18255 RVA: 0x000FBF4D File Offset: 0x000FA14D
		internal static IEqualityComparer<CompositeKey> CreateComparer(KeyManager keyManager)
		{
			return new CompositeKey.CompositeKeyComparer(keyManager);
		}

		// Token: 0x06004750 RID: 18256 RVA: 0x000FBF58 File Offset: 0x000FA158
		internal CompositeKey Merge(KeyManager keyManager, CompositeKey other)
		{
			PropagatorResult[] array = new PropagatorResult[this.KeyComponents.Length];
			for (int i = 0; i < this.KeyComponents.Length; i++)
			{
				array[i] = this.KeyComponents[i].Merge(keyManager, other.KeyComponents[i]);
			}
			return new CompositeKey(array);
		}

		// Token: 0x04001952 RID: 6482
		internal readonly PropagatorResult[] KeyComponents;

		// Token: 0x02000BF0 RID: 3056
		private class CompositeKeyComparer : IEqualityComparer<CompositeKey>
		{
			// Token: 0x06006892 RID: 26770 RVA: 0x001645D1 File Offset: 0x001627D1
			internal CompositeKeyComparer(KeyManager manager)
			{
				this._manager = manager;
			}

			// Token: 0x06006893 RID: 26771 RVA: 0x001645E0 File Offset: 0x001627E0
			public bool Equals(CompositeKey left, CompositeKey right)
			{
				if (left == right)
				{
					return true;
				}
				if (left == null || right == null)
				{
					return false;
				}
				if (left.KeyComponents.Length != right.KeyComponents.Length)
				{
					return false;
				}
				for (int i = 0; i < left.KeyComponents.Length; i++)
				{
					PropagatorResult propagatorResult = left.KeyComponents[i];
					PropagatorResult propagatorResult2 = right.KeyComponents[i];
					if (propagatorResult.Identifier != -1)
					{
						if (propagatorResult2.Identifier == -1 || this._manager.GetCliqueIdentifier(propagatorResult.Identifier) != this._manager.GetCliqueIdentifier(propagatorResult2.Identifier))
						{
							return false;
						}
					}
					else if (propagatorResult2.Identifier != -1 || !ByValueEqualityComparer.Default.Equals(propagatorResult.GetSimpleValue(), propagatorResult2.GetSimpleValue()))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06006894 RID: 26772 RVA: 0x00164690 File Offset: 0x00162890
			public int GetHashCode(CompositeKey key)
			{
				int num = 0;
				foreach (PropagatorResult propagatorResult in key.KeyComponents)
				{
					num = (num << 5) ^ this.GetComponentHashCode(propagatorResult);
				}
				return num;
			}

			// Token: 0x06006895 RID: 26773 RVA: 0x001646C8 File Offset: 0x001628C8
			private int GetComponentHashCode(PropagatorResult keyComponent)
			{
				if (keyComponent.Identifier == -1)
				{
					return ByValueEqualityComparer.Default.GetHashCode(keyComponent.GetSimpleValue());
				}
				return this._manager.GetCliqueIdentifier(keyComponent.Identifier).GetHashCode();
			}

			// Token: 0x04002F2A RID: 12074
			private readonly KeyManager _manager;
		}
	}
}
