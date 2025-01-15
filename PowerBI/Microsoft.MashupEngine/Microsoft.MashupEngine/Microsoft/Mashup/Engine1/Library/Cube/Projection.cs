using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D3C RID: 3388
	internal class Projection
	{
		// Token: 0x06005B07 RID: 23303 RVA: 0x0013DCC0 File Offset: 0x0013BEC0
		public static Projection FromView(Set set, View view)
		{
			IdentifierCubeExpression[] array = (from o in set.GetResultObjects()
				select ((ICubeObject2)o).Identifier).ToArray<IdentifierCubeExpression>();
			IdentifierCubeExpression[] array2 = new IdentifierCubeExpression[view.Keys.Length];
			for (int i = 0; i < view.Keys.Length; i++)
			{
				array2[i] = array[view.GetColumn(i)];
			}
			return new Projection(view.Keys, array2);
		}

		// Token: 0x06005B08 RID: 23304 RVA: 0x0013DD40 File Offset: 0x0013BF40
		public Projection(Keys keys, IdentifierCubeExpression[] identifiers)
		{
			this.keys = keys;
			this.identifiers = identifiers;
		}

		// Token: 0x17001AF6 RID: 6902
		// (get) Token: 0x06005B09 RID: 23305 RVA: 0x0013DD56 File Offset: 0x0013BF56
		public Keys Keys
		{
			get
			{
				return this.keys;
			}
		}

		// Token: 0x17001AF7 RID: 6903
		// (get) Token: 0x06005B0A RID: 23306 RVA: 0x0013DD5E File Offset: 0x0013BF5E
		public IdentifierCubeExpression[] Identifiers
		{
			get
			{
				return this.identifiers;
			}
		}

		// Token: 0x06005B0B RID: 23307 RVA: 0x0013DD68 File Offset: 0x0013BF68
		public Projection Add(string name, IdentifierCubeExpression identifier)
		{
			KeysBuilder keysBuilder = default(KeysBuilder);
			ArrayBuilder<IdentifierCubeExpression> arrayBuilder = default(ArrayBuilder<IdentifierCubeExpression>);
			for (int i = 0; i < this.keys.Length; i++)
			{
				keysBuilder.Add(this.keys[i]);
				arrayBuilder.Add(this.identifiers[i]);
			}
			keysBuilder.Add(name);
			arrayBuilder.Add(identifier);
			return new Projection(keysBuilder.ToKeys(), arrayBuilder.ToArray());
		}

		// Token: 0x06005B0C RID: 23308 RVA: 0x0013DDE0 File Offset: 0x0013BFE0
		public Projection ProjectColumns(ColumnSelection selection)
		{
			IdentifierCubeExpression[] array = new IdentifierCubeExpression[selection.Keys.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.identifiers[selection.GetColumn(i)];
			}
			return new Projection(selection.Keys, array);
		}

		// Token: 0x06005B0D RID: 23309 RVA: 0x0013DE2C File Offset: 0x0013C02C
		public Projection Append(Projection other)
		{
			KeysBuilder keysBuilder = default(KeysBuilder);
			ArrayBuilder<IdentifierCubeExpression> arrayBuilder = default(ArrayBuilder<IdentifierCubeExpression>);
			for (int i = 0; i < this.keys.Length; i++)
			{
				keysBuilder.Add(this.keys[i]);
				arrayBuilder.Add(this.identifiers[i]);
			}
			for (int j = 0; j < other.keys.Length; j++)
			{
				keysBuilder.Add(other.keys[j]);
				arrayBuilder.Add(other.identifiers[j]);
			}
			return new Projection(keysBuilder.ToKeys(), arrayBuilder.ToArray());
		}

		// Token: 0x06005B0E RID: 23310 RVA: 0x0013DECC File Offset: 0x0013C0CC
		public View ToView(Set set)
		{
			Dictionary<IdentifierCubeExpression, int> dictionary = new Dictionary<IdentifierCubeExpression, int>();
			foreach (IdentifierCubeExpression identifierCubeExpression in from o in set.GetResultObjects()
				select ((ICubeObject2)o).Identifier)
			{
				dictionary.Add(identifierCubeExpression, dictionary.Count);
			}
			View.Builder builder = default(View.Builder);
			for (int i = 0; i < this.keys.Length; i++)
			{
				builder.Add(this.keys[i], dictionary[this.identifiers[i]]);
			}
			return builder.ToView();
		}

		// Token: 0x040032D2 RID: 13010
		private readonly Keys keys;

		// Token: 0x040032D3 RID: 13011
		private readonly IdentifierCubeExpression[] identifiers;
	}
}
