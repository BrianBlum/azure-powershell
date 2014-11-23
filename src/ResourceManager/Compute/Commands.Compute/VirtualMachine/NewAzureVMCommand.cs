﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using System.Management.Automation;
using AutoMapper;
using Microsoft.Azure.Commands.Compute.Common;
using Microsoft.Azure.Management.Compute;
using Microsoft.Azure.Management.Compute.Models;
using Microsoft.WindowsAzure;
using PSM = Microsoft.Azure.Commands.Compute.Models;

namespace Microsoft.Azure.Commands.Compute
{
    /// <summary>
    /// Creates a new resource.
    /// </summary>
    [Cmdlet(VerbsCommon.New, ProfileNouns.VirtualMachine), OutputType(typeof(PSM.VirtualMachine))]
    public class NewAzureVMCommand : VirtualMachineBaseCmdlet
    {

        [Parameter(
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The Location.")]
        [ValidateNotNullOrEmpty]
        public string Location { get; set; }

        [Parameter(
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The VM Profile.")]
        [ValidateNotNullOrEmpty]
        public PSM.PSVirtualMachineProfile VMProfile { get; set; }

        public override void ExecuteCmdlet()
        {
            base.ExecuteCmdlet();

            VirtualMachineProperties vmProps = Mapper.Map<VirtualMachineProperties>(this.VMProfile);

            var parameters = new VirtualMachineCreateOrUpdateParameters
            {
                VirtualMachine = new VirtualMachine
                {
                    VirtualMachineProperties = vmProps,
                    Location = this.Location,
                    Name = this.Name
                }
            };

            this.VirtualMachineClient.CreateOrUpdate(this.ResourceGroupName, parameters);
        }
    }
}
