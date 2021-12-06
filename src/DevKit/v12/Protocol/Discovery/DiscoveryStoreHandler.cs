﻿//----------------------------------------------------------------------- 
// ETP DevKit, 1.2
//
// Copyright 2019 Energistics
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//   
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Energistics.Etp.Common;
using Energistics.Etp.Common.Datatypes;
using Energistics.Etp.v12.Datatypes.Object;

namespace Energistics.Etp.v12.Protocol.Discovery
{
    /// <summary>
    /// Base implementation of the <see cref="IDiscoveryStore"/> interface.
    /// </summary>
    /// <seealso cref="Etp12ProtocolHandler" />
    /// <seealso cref="Energistics.Etp.v12.Protocol.Discovery.IDiscoveryStore" />
    public class DiscoveryStoreHandler : Etp12ProtocolHandler<CapabilitiesStore, ICapabilitiesStore, CapabilitiesCustomer, ICapabilitiesCustomer>, IDiscoveryStore
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DiscoveryStoreHandler"/> class.
        /// </summary>
        public DiscoveryStoreHandler() : base((int)Protocols.Discovery, Roles.Store, Roles.Customer)
        {
            RegisterMessageHandler<GetResources>(Protocols.Discovery, MessageTypes.Discovery.GetResources, HandleGetResources);
            RegisterMessageHandler<GetDeletedResources>(Protocols.Discovery, MessageTypes.Discovery.GetDeletedResources, HandleGetDeletedResources);
        }

        /// <summary>
        /// Handles the GetResources event from a customer.
        /// </summary>
        public event EventHandler<DualListRequestEventArgs<GetResources, Resource, Edge>> OnGetResources;

        /// <summary>
        /// Sends a GetResourcesResponse message to a customer.
        /// </summary>
        /// <param name="correlatedHeader">The message header that the messages to send are correlated with.</param>
        /// <param name="resources">The list of <see cref="Resource"/> objects.</param>
        /// <param name="isFinalPart">Whether or not this is the final part of a multi-part message.</param>
        /// <param name="extension">The message header extension.</param>
        /// <returns>The sent message on success; <c>null</c> otherwise.</returns>
        public virtual EtpMessage<GetResourcesResponse> GetResourcesResponse(IMessageHeader correlatedHeader, IList<Resource> resources, bool isFinalPart = true, IMessageHeaderExtension extension = null)
        {
            var body = new GetResourcesResponse
            {
                Resources = resources ?? new List<Resource>(),
            };

            return SendResponse(body, correlatedHeader, extension: extension, isMultiPart: true, isFinalPart: isFinalPart);
        }

        /// <summary>
        /// Sends a GetResourcesEdgeResponse message to a customer.
        /// </summary>
        /// <param name="correlatedHeader">The message header that the messages to send are correlated with.</param>
        /// <param name="edges">The list of <see cref="Edge"/> objects.</param>
        /// <param name="isFinalPart">Whether or not this is the final part of a multi-part message.</param>
        /// <param name="extension">The message header extension.</param>
        /// <returns>The sent message on success; <c>null</c> otherwise.</returns>
        public virtual EtpMessage<GetResourcesEdgesResponse> GetResourcesEdgesResponse(IMessageHeader correlatedHeader, IList<Edge> edges, bool isFinalPart = true, IMessageHeaderExtension extension = null)
        {
            var body = new GetResourcesEdgesResponse
            {
                Edges = edges ?? new List<Edge>(),
            };

            return SendResponse(body, correlatedHeader, extension: extension, isMultiPart: true, isFinalPart: isFinalPart);
        }

        /// <summary>
        /// Sends a complete multi-part set of GetResourcesResponse and GetResourcesEdgesResponse messagess to a customer.
        /// If there are no resources, an empty GetResourcesResponse message is sent.
        /// If there are no edges, no GetResourcesEdgesResponse message is sent.
        /// </summary>
        /// <param name="correlatedHeader">The message header that the messages to send are correlated with.</param>
        /// <param name="resources">The list of <see cref="Resource"/> objects.</param>
        /// <param name="setFinalPart">Whether or not the final part flag should be set on the last message.</param>
        /// <param name="resourcesExtension">The message header extension for the GetResourcesResponse message.</param>
        /// <param name="edgesExtension">The message header extension for the GetResourcesEdgesResponse message.</param>
        /// <returns>The sent message on success; <c>null</c> otherwise.</returns>
        public virtual EtpMessage<GetResourcesResponse> GetResourcesResponse(IMessageHeader correlatedHeader, IList<Resource> resources, IList<Edge> edges, bool setFinalPart = true, IMessageHeaderExtension resourcesExtension = null, IMessageHeaderExtension edgesExtension = null)
        {
            var message = GetResourcesResponse(correlatedHeader, resources, isFinalPart: ((edges == null || edges.Count == 0) && setFinalPart), extension: resourcesExtension);
            if (message == null)
                return null;

            if (edges?.Count > 0)
            {
                var ret = GetResourcesEdgesResponse(correlatedHeader, edges, isFinalPart: setFinalPart, extension: edgesExtension);
                if (ret == null)
                    return null;
            }

            return message;
        }

        /// <summary>
        /// Handles the GetDeletedResources event from a customer.
        /// </summary>
        public event EventHandler<ListRequestEventArgs<GetDeletedResources, DeletedResource>> OnGetDeletedResources;

        /// <summary>
        /// Sends a GetDeletedResourcesResponse message to a customer.
        /// </summary>
        /// <param name="correlatedHeader">The message header that the messages to send are correlated with.</param>
        /// <param name="deletedResources">The list of <see cref="DeletedResource"/> objects.</param>
        /// <param name="isFinalPart">Whether or not this is the final part of a multi-part message.</param>
        /// <param name="extension">The message header extension.</param>
        /// <returns>The sent message on success; <c>null</c> otherwise.</returns>
        public virtual EtpMessage<GetDeletedResourcesResponse> GetDeletedResourcesResponse(IMessageHeader correlatedHeader, IList<DeletedResource> deletedResources, bool isFinalPart = true, IMessageHeaderExtension extension = null)
        {
            var body = new GetDeletedResourcesResponse
            {
                DeletedResources = deletedResources ?? new List<DeletedResource>(),
            };

            return SendResponse(body, correlatedHeader, extension: extension, isMultiPart: true, isFinalPart: isFinalPart);
        }

        /// <summary>
        /// Handles the GetResources message from a customer.
        /// </summary>
        /// <param name="message">The GetResources message.</param>
        protected virtual void HandleGetResources(EtpMessage<GetResources> message)
        {
            HandleRequestMessage(message, OnGetResources, HandleGetResources,
                responseMethod: (args) => GetResourcesResponse(args.Request?.Header, args.Responses1, args.Responses2, setFinalPart: !args.HasErrors, resourcesExtension: args.Response1Extension, edgesExtension: args.Response2Extension));
        }

        /// <summary>
        /// Handles the GetResources message from a customer.
        /// </summary>
        /// <param name="args">The <see cref="DualListRequestEventArgs{GetResources, Resource, Edge}"/> instance containing the event data.</param>
        protected virtual void HandleGetResources(DualListRequestEventArgs<GetResources, Resource, Edge> args)
        {
        }

        /// <summary>
        /// Handles the GetDeletedResources message from a customer.
        /// </summary>
        /// <param name="message">The GetDeletedResources message.</param>
        protected virtual void HandleGetDeletedResources(EtpMessage<GetDeletedResources> message)
        {
            HandleRequestMessage(message, OnGetDeletedResources, HandleGetDeletedResources,
                responseMethod: (args) => GetDeletedResourcesResponse(args.Request?.Header, args.Responses, isFinalPart: !args.HasErrors, extension: args.ResponseExtension));
        }

        /// <summary>
        /// Handles the GetDeletedResources message from a customer.
        /// </summary>
        /// <param name="args">The <see cref="ListRequestEventArgs{GetDeletedResources, DeletedResource}"/> instance containing the event data.</param>
        protected virtual void HandleGetDeletedResources(ListRequestEventArgs<GetDeletedResources, DeletedResource> args)
        {
        }
    }
}
