using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200016C RID: 364
	public static class ActivityExplorer
	{
		// Token: 0x06000981 RID: 2433 RVA: 0x00020950 File Offset: 0x0001EB50
		public static IEnumerable<ActivityTypeRecord> GetAllActivityTypes([NotNull] string path, bool recursive)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<string>(path, "path");
			IEnumerable<ActivityTypeRecord> activityRecords = null;
			ExtendedAppDomain.RunWithPrivateAppDomainWorker<ActivityExplorer.ActivityExplorerWorker>(delegate(ActivityExplorer.ActivityExplorerWorker explorer)
			{
				activityRecords = explorer.GetAllActivityTypes(path, recursive);
			}, Path.GetDirectoryName(typeof(ActivityExplorer.ActivityExplorerWorker).Assembly.Location));
			return activityRecords;
		}

		// Token: 0x02000634 RID: 1588
		private class ActivityExplorerWorker : MarshalByRefObject
		{
			// Token: 0x06002CC6 RID: 11462 RVA: 0x0009F6B0 File Offset: 0x0009D8B0
			public List<ActivityTypeRecord> GetAllActivityTypes(string path, bool recursive)
			{
				List<Type> list = new List<Type>();
				HashSet<Assembly> hashSet = new HashSet<Assembly>(from a in AppDomain.CurrentDomain.GetAssemblies()
					where !a.IsDynamic
					select a);
				using (IEnumerator<string> enumerator = AssemblyWalker.GetAssemblyFileNames(path, recursive, new Predicate<string>(ActivityExplorer.ActivityExplorerWorker.AssemblyHasActivities)).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string assemblyFileName = enumerator.Current;
						Assembly assembly = hashSet.FirstOrDefault((Assembly a) => Path.GetFileName(a.Location).Equals(Path.GetFileName(assemblyFileName), StringComparison.OrdinalIgnoreCase)) ?? Assembly.LoadFrom(assemblyFileName);
						try
						{
							list.AddRange(assembly.GetTypes());
						}
						catch (ReflectionTypeLoadException ex)
						{
							list.AddRange(ex.Types);
						}
					}
				}
				Type typeFromHandle = typeof(ActivityType);
				List<ActivityTypeRecord> list2 = new List<ActivityTypeRecord>();
				foreach (Type type in list.Where(new Func<Type, bool>(typeFromHandle.IsAssignableFrom)))
				{
					if ((from ctor in type.GetConstructors()
						where ctor.GetParameters().None<ParameterInfo>()
						select ctor).Any<ConstructorInfo>())
					{
						ActivityType activityType = (ActivityType)DynamicLoader.Instantiate(type, new Predicate<Type>(DynamicLoader.IsValidType), null);
						list2.Add(new ActivityTypeRecord(activityType.ShortName, type.Name, type.Assembly.GetName().Name, type.Name, type.Name, type.Namespace));
					}
				}
				return list2;
			}

			// Token: 0x06002CC7 RID: 11463 RVA: 0x0009F88C File Offset: 0x0009DA8C
			public static bool AssemblyHasActivities(string filename)
			{
				return AssemblyWalker.AssemblyHasResourceName(filename, "Microsoft.Cloud.Platform.Activities.Defined");
			}

			// Token: 0x04001181 RID: 4481
			private const string c_activityResourceName = "Microsoft.Cloud.Platform.Activities.Defined";
		}
	}
}
