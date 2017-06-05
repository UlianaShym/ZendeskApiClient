﻿using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi.Client.Formatters;
using ZendeskApi.Client.Options;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class TicketResource : ZendeskResource<Ticket>, ITicketResource
    {
        private const string ResourceUri = "/api/v2/tickets";

        public TicketResource(ZendeskOptions options) : base(options) { }
        
        public async Task<IResponse<Ticket>> GetAsync(long id)
        {
            using (var client = CreateZendeskClient(ResourceUri + "/"))
            {
                var response = await client.GetAsync(id.ToString()).ConfigureAwait(false);
                return await response.Content.ReadAsAsync<TicketResponse>();
            }
        }

        public async Task<IListResponse<Ticket>> GetAllAsync(List<long> ids)
        {
            using (var client = CreateZendeskClient(ResourceUri + "/"))
            {
                var response = await client.GetAsync($"show_many?ids={ZendeskFormatter.ToCsv(ids)}").ConfigureAwait(false);
                return await response.Content.ReadAsAsync<TicketListResponse>();
            }
        }

        public async Task<IResponse<Ticket>> PutAsync(TicketRequest request)
        {
            using (var client = CreateZendeskClient(ResourceUri + "/"))
            {
                var response = await client.PutAsJsonAsync(request.Item.Id.ToString(), request).ConfigureAwait(false);
                return await response.Content.ReadAsAsync<TicketResponse>();
            }
        }

        public async Task<IResponse<Ticket>> PostAsync(TicketRequest request)
        {
            using (var client = CreateZendeskClient("/"))
            {
                var response = await client.PostAsJsonAsync(ResourceUri, request).ConfigureAwait(false);
                return await response.Content.ReadAsAsync<TicketResponse>();
            }
        }

        public Task DeleteAsync(long id)
        {
            using (var client = CreateZendeskClient(ResourceUri + "/"))
            {
                return client.DeleteAsync(id.ToString());
            }
        }
    }
}
