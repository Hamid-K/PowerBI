using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D41 RID: 3393
	internal class ScopeCube : ICube
	{
		// Token: 0x06005B33 RID: 23347 RVA: 0x0013EBB7 File Offset: 0x0013CDB7
		public ScopeCube(IDictionary<string, ICube> scopeCubes)
		{
			this.scopeCubes = scopeCubes;
		}

		// Token: 0x17001AFA RID: 6906
		// (get) Token: 0x06005B34 RID: 23348 RVA: 0x0013EBC6 File Offset: 0x0013CDC6
		public IEnumerable<string> Scopes
		{
			get
			{
				return this.scopeCubes.Keys;
			}
		}

		// Token: 0x17001AFB RID: 6907
		public ICube this[string index]
		{
			get
			{
				return this.scopeCubes[index];
			}
		}

		// Token: 0x17001AFC RID: 6908
		// (get) Token: 0x06005B36 RID: 23350 RVA: 0x0013EBE4 File Offset: 0x0013CDE4
		public IdentifierCubeExpression Identifier
		{
			get
			{
				if (this.identifier == null)
				{
					IEnumerable<string> enumerable = from i in this.scopeCubes.Select(delegate(KeyValuePair<string, ICube> sc)
						{
							if (!(sc.Key == ScopeCube.unscopedScope))
							{
								return sc.Value.Identifier.NewScope(sc.Key);
							}
							return sc.Value.Identifier;
						})
						select i.Identifier into s
						orderby s
						select s;
					this.identifier = new IdentifierCubeExpression("(" + string.Join(",", enumerable.ToArray<string>()) + ")");
				}
				return this.identifier;
			}
		}

		// Token: 0x06005B37 RID: 23351 RVA: 0x0013ECA0 File Offset: 0x0013CEA0
		public bool TryGetObject(IdentifierCubeExpression identifier, out ICubeObject obj)
		{
			ScopePath scopePath;
			IdentifierCubeExpression unscoped = identifier.GetUnscoped(out scopePath);
			if (this.scopeCubes[ScopeCube.unscopedScope].TryGetObject(unscoped, out obj))
			{
				obj = obj.NewScopePath(scopePath);
				return true;
			}
			obj = null;
			return false;
		}

		// Token: 0x040032DD RID: 13021
		public static readonly string unscopedScope = string.Empty;

		// Token: 0x040032DE RID: 13022
		private readonly IDictionary<string, ICube> scopeCubes;

		// Token: 0x040032DF RID: 13023
		private IdentifierCubeExpression identifier;
	}
}
