import { CustomerPageClient } from "@/components/routes/customer-page/client";
import { api } from "@/lib/api";
import { SingleCustomer } from "@/types/customer";

export default async function CustomerPage({
  params,
}: {
  params: Promise<{ customerId: string }>;
}) {
  const { customerId } = await params;
  const response = await api.get(`/Customers/${customerId}`);

  const customer = response.data.data.Data as SingleCustomer;
  return (
    <div className="flex-col">
      <div className="flex-1 p-8 pt-6 space-y-4">
        <CustomerPageClient customer={customer} />
      </div>
    </div>
  );
}
