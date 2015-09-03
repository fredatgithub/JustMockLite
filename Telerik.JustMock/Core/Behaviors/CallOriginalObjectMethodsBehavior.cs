/*
 JustMock Lite
 Copyright © 2010-2015 Telerik AD

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

   http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

using System;
using System.Linq;
using System.Reflection;

namespace Telerik.JustMock.Core.Behaviors
{
	internal class CallOriginalObjectMethodsBehavior : IBehavior
	{
		public void Process(Invocation invocation)
		{
			var method = invocation.Member as MethodBase;

			if (method != null
				&& method.IsVirtual
				&& typeof(object).GetMethods().Any(m =>
					m.Name == method.Name
					&& m.ReturnType == invocation.ReturnType
					&& m.GetParameters().Select(p => p.ParameterType).SequenceEqual(method.GetParameters().Select(p => p.ParameterType))
				))
			{
				invocation.CallOriginal = true;
				invocation.UserProvidedImplementation = true;
			}
		}
	}
}
