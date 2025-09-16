import { CustomerCard } from "@/components/routes/dashboard/customer-card";
import { Heading } from "@/components/shared/heading";
import { Separator } from "@/components/ui/separator";
import { api } from "@/lib/api";
import { Customer } from "@/types/customer";

export default async function CustomerList() {
  const response = await api.get("/Customers");
  const customers = response.data.data.Data as Customer[];
  return (
    <div className="flex-col">
      <div className="flex-1 p-8 pt-6 space-y-4">
        <Heading
          title={`Customers (${customers.length})`}
          description="A list of all customers"
        />
        <Separator />
        <div className="grid gap-6 md:grid-cols-2 lg:grid-cols-3">
          {customers.map((customer) => (
            <CustomerCard key={customer.customerId} customer={customer} />
          ))}
        </div>
      </div>
    </div>
  );
}
