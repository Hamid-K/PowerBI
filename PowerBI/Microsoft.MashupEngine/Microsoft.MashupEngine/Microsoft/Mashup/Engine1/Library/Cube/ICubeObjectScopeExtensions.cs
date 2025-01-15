using System;
using System.Collections.Generic;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D37 RID: 3383
	internal static class ICubeObjectScopeExtensions
	{
		// Token: 0x06005AE4 RID: 23268 RVA: 0x0013D73E File Offset: 0x0013B93E
		public static ICube NewScope(this ICube cube, string scope)
		{
			return new ScopeCube(new Dictionary<string, ICube>
			{
				{ scope, cube },
				{
					ScopeCube.unscopedScope,
					cube.GetUnscoped()
				}
			});
		}

		// Token: 0x06005AE5 RID: 23269 RVA: 0x0013D763 File Offset: 0x0013B963
		public static ICube Union(this ICube cube1, ICube cube2)
		{
			Dictionary<string, ICube> dictionary = new Dictionary<string, ICube>();
			ICubeObjectScopeExtensions.UnionScopeCube(dictionary, cube1);
			ICubeObjectScopeExtensions.UnionScopeCube(dictionary, cube2);
			return new ScopeCube(dictionary);
		}

		// Token: 0x06005AE6 RID: 23270 RVA: 0x0013D780 File Offset: 0x0013B980
		public static ICube GetUnscoped(this ICube cube)
		{
			ScopeCube scopeCube = cube as ScopeCube;
			if (scopeCube != null)
			{
				cube = scopeCube[ScopeCube.unscopedScope];
			}
			return cube;
		}

		// Token: 0x06005AE7 RID: 23271 RVA: 0x0013D7A8 File Offset: 0x0013B9A8
		public static ICubeObject NewScope(this ICubeObject cubeObj, string scope)
		{
			switch (cubeObj.Kind)
			{
			case CubeObjectKind.DimensionAttribute:
				return ((ICubeLevel)cubeObj).NewScope(scope);
			case CubeObjectKind.Property:
				return ((ICubeProperty)cubeObj).NewScope(scope);
			case CubeObjectKind.Measure:
				return ((ICubeMeasure)cubeObj).NewScope(scope);
			default:
			{
				ICubeHierarchy cubeHierarchy = cubeObj as ICubeHierarchy;
				if (cubeHierarchy != null)
				{
					return cubeHierarchy.NewScope(scope);
				}
				throw new NotSupportedException();
			}
			}
		}

		// Token: 0x06005AE8 RID: 23272 RVA: 0x0013D80E File Offset: 0x0013BA0E
		public static ICubeHierarchy NewScope(this ICubeHierarchy hierarchy, string scope)
		{
			return new ScopeCubeHierarchy(hierarchy, scope);
		}

		// Token: 0x06005AE9 RID: 23273 RVA: 0x0013D817 File Offset: 0x0013BA17
		public static ICubeLevel NewScope(this ICubeLevel level, string scope)
		{
			return new ScopeCubeLevel(level, scope);
		}

		// Token: 0x06005AEA RID: 23274 RVA: 0x0013D820 File Offset: 0x0013BA20
		public static ICubeProperty NewScope(this ICubeProperty property, string scope)
		{
			return new ScopeCubeProperty(property, scope);
		}

		// Token: 0x06005AEB RID: 23275 RVA: 0x0013D829 File Offset: 0x0013BA29
		public static ICubeMeasure NewScope(this ICubeMeasure measure, string scope)
		{
			return new ScopeCubeMeasure(measure, scope);
		}

		// Token: 0x06005AEC RID: 23276 RVA: 0x0013D834 File Offset: 0x0013BA34
		public static ICubeObject GetUnscoped(this ICubeObject cubeObj, out ScopePath scopePath)
		{
			CubeObjectKind kind = cubeObj.Kind;
			if (kind == CubeObjectKind.DimensionAttribute)
			{
				return ((ICubeLevel)cubeObj).GetUnscoped(out scopePath);
			}
			if (kind == CubeObjectKind.Measure)
			{
				return ((ICubeMeasure)cubeObj).GetUnscoped(out scopePath);
			}
			ICubeHierarchy cubeHierarchy = cubeObj as ICubeHierarchy;
			if (cubeHierarchy != null)
			{
				return cubeHierarchy.GetUnscoped(out scopePath);
			}
			throw new NotSupportedException();
		}

		// Token: 0x06005AED RID: 23277 RVA: 0x0013D884 File Offset: 0x0013BA84
		public static ICubeHierarchy GetUnscoped(this ICubeHierarchy hierarchy, out ScopePath scopePath)
		{
			ArrayBuilder<string> arrayBuilder = default(ArrayBuilder<string>);
			for (;;)
			{
				ScopeCubeHierarchy scopeCubeHierarchy = hierarchy as ScopeCubeHierarchy;
				if (scopeCubeHierarchy == null)
				{
					break;
				}
				arrayBuilder.Add(scopeCubeHierarchy.Scope);
				hierarchy = scopeCubeHierarchy.Hierarchy;
			}
			scopePath = new ScopePath(arrayBuilder.ToArray());
			return hierarchy;
		}

		// Token: 0x06005AEE RID: 23278 RVA: 0x0013D8CC File Offset: 0x0013BACC
		public static ICubeLevel GetUnscoped(this ICubeLevel level, out ScopePath scopePath)
		{
			ArrayBuilder<string> arrayBuilder = default(ArrayBuilder<string>);
			for (;;)
			{
				ScopeCubeLevel scopeCubeLevel = level as ScopeCubeLevel;
				if (scopeCubeLevel == null)
				{
					break;
				}
				arrayBuilder.Add(scopeCubeLevel.Scope);
				level = scopeCubeLevel.Level;
			}
			scopePath = new ScopePath(arrayBuilder.ToArray());
			return level;
		}

		// Token: 0x06005AEF RID: 23279 RVA: 0x0013D914 File Offset: 0x0013BB14
		public static ICubeMeasure GetUnscoped(this ICubeMeasure measure, out ScopePath scopePath)
		{
			ArrayBuilder<string> arrayBuilder = default(ArrayBuilder<string>);
			for (;;)
			{
				ScopeCubeMeasure scopeCubeMeasure = measure as ScopeCubeMeasure;
				if (scopeCubeMeasure == null)
				{
					break;
				}
				arrayBuilder.Add(scopeCubeMeasure.Scope);
				measure = scopeCubeMeasure.Measure;
			}
			scopePath = new ScopePath(arrayBuilder.ToArray());
			return measure;
		}

		// Token: 0x06005AF0 RID: 23280 RVA: 0x0013D95C File Offset: 0x0013BB5C
		public static ICubeObject NewScopePath(this ICubeObject cubeObj, ScopePath scopePath)
		{
			switch (cubeObj.Kind)
			{
			case CubeObjectKind.DimensionAttribute:
				return ((ICubeLevel)cubeObj).NewScopePath(scopePath);
			case CubeObjectKind.Property:
				return ((ICubeProperty)cubeObj).NewScopePath(scopePath);
			case CubeObjectKind.Measure:
				return ((ICubeMeasure)cubeObj).NewScopePath(scopePath);
			default:
			{
				ICubeHierarchy cubeHierarchy = cubeObj as ICubeHierarchy;
				if (cubeHierarchy != null)
				{
					return cubeHierarchy.NewScopePath(scopePath);
				}
				throw new NotSupportedException();
			}
			}
		}

		// Token: 0x06005AF1 RID: 23281 RVA: 0x0013D9C4 File Offset: 0x0013BBC4
		public static ICubeLevel NewScopePath(this ICubeLevel level, ScopePath scopePath)
		{
			for (int i = scopePath.Path.Length - 1; i >= 0; i--)
			{
				level = level.NewScope(scopePath.Path[i]);
			}
			return level;
		}

		// Token: 0x06005AF2 RID: 23282 RVA: 0x0013D9F8 File Offset: 0x0013BBF8
		public static ICubeHierarchy NewScopePath(this ICubeHierarchy hierarchy, ScopePath scopePath)
		{
			for (int i = scopePath.Path.Length - 1; i >= 0; i--)
			{
				hierarchy = hierarchy.NewScope(scopePath.Path[i]);
			}
			return hierarchy;
		}

		// Token: 0x06005AF3 RID: 23283 RVA: 0x0013DA2C File Offset: 0x0013BC2C
		public static ICubeProperty NewScopePath(this ICubeProperty property, ScopePath scopePath)
		{
			for (int i = scopePath.Path.Length - 1; i >= 0; i--)
			{
				property = property.NewScope(scopePath.Path[i]);
			}
			return property;
		}

		// Token: 0x06005AF4 RID: 23284 RVA: 0x0013DA60 File Offset: 0x0013BC60
		public static ICubeMeasure NewScopePath(this ICubeMeasure measure, ScopePath scopePath)
		{
			for (int i = scopePath.Path.Length - 1; i >= 0; i--)
			{
				measure = measure.NewScope(scopePath.Path[i]);
			}
			return measure;
		}

		// Token: 0x06005AF5 RID: 23285 RVA: 0x0013DA94 File Offset: 0x0013BC94
		public static ICubeObject ReplaceScopePaths(this ICubeObject cubeObj, IDictionary<ScopePath, ScopePath> replacements)
		{
			ScopePath scopePath;
			ICubeObject unscoped = cubeObj.GetUnscoped(out scopePath);
			ScopePath scopePath2;
			if (replacements.TryGetValue(scopePath, out scopePath2))
			{
				cubeObj = unscoped.NewScopePath(scopePath2);
			}
			return cubeObj;
		}

		// Token: 0x06005AF6 RID: 23286 RVA: 0x0013DAC0 File Offset: 0x0013BCC0
		public static ICubeLevel ReplaceScopePaths(this ICubeLevel level, IDictionary<ScopePath, ScopePath> replacements)
		{
			ScopePath scopePath;
			ICubeLevel unscoped = level.GetUnscoped(out scopePath);
			ScopePath scopePath2;
			if (replacements.TryGetValue(scopePath, out scopePath2))
			{
				level = unscoped.NewScopePath(scopePath2);
			}
			return level;
		}

		// Token: 0x06005AF7 RID: 23287 RVA: 0x0013DAEC File Offset: 0x0013BCEC
		private static void UnionScopeCube(Dictionary<string, ICube> scopeCubes, ICube cube)
		{
			ScopeCube scopeCube = cube as ScopeCube;
			if (scopeCube != null)
			{
				using (IEnumerator<string> enumerator = scopeCube.Scopes.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string text = enumerator.Current;
						ICubeObjectScopeExtensions.UnionScopeCube(scopeCubes, text, scopeCube[text]);
					}
					return;
				}
			}
			ICubeObjectScopeExtensions.UnionScopeCube(scopeCubes, ScopeCube.unscopedScope, cube);
		}

		// Token: 0x06005AF8 RID: 23288 RVA: 0x0013DB58 File Offset: 0x0013BD58
		private static void UnionScopeCube(Dictionary<string, ICube> scopeCubes, string scope, ICube cube)
		{
			ICube cube2;
			if (scopeCubes.TryGetValue(scope, out cube2) && cube2 != cube)
			{
				throw new NotSupportedException();
			}
			scopeCubes[scope] = cube;
		}
	}
}
