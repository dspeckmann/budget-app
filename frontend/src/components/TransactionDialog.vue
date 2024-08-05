<script setup lang="ts">
import { ref } from 'vue'
import Modal from './controls/Modal.vue'
import { PlusIcon, PencilIcon, TrashIcon } from '@heroicons/vue/24/outline'
import { months } from '../domain/months'

defineProps<{
  month: number
  year: number
  category: string
}>()

const transactions = ref([
  {
    name: 'Edeka',
    amount: 20.5
  },
  {
    name: 'Rewe Center',
    amount: 399.99
  }
])

// TODO: Move to central place
const formatAmount = (amount: number) => {
  return new Intl.NumberFormat('de-DE', { style: 'currency', currency: 'EUR' }).format(
    amount,
  )
}

const amountInput = ref<HTMLInputElement | undefined>()
const newName = ref('')
const newAmount = ref('')

const onSubmit = () => {
  transactions.value.push({ name: newName.value, amount: Number(newAmount.value) })
  newName.value = ''
  newAmount.value = ''
  amountInput.value?.focus()
}

const onDelete = (transaction: any) => {
  transactions.value = transactions.value.filter(t => t != transaction)
}
</script>

<template>
  <Modal>
    <template #title>
      <strong>{{ category }}</strong> im <strong>{{ months[month] }} {{ year }}</strong>
    </template>
    <template #default>
      <form @submit.prevent="onSubmit" class="flex flex-row gap-1 mb-4 align-center">
        <input ref="amountInput" v-model="newName" autofocus required />
        <input type="number" step="0.01" v-model="newAmount" required />
        <button type="submit" class="success"><PlusIcon class="size-4" /></button>
      </form>
      <table>
        <tr v-for="transaction in transactions">
          <td class="text-left">{{ transaction.name }}</td>
          <td class="text-right">{{ formatAmount(transaction.amount) }}</td>
          <td style="width: 0%; white-space: nowrap;">
            <button class="mr-1"><PencilIcon class="size-4" /></button>
            <button class="danger" @click="onDelete(transaction)"><TrashIcon class="size-4" /></button>
          </td>
        </tr>
      </table>
    </template>
  </Modal>
</template>

<style scoped>
td, th {
  @apply py-2 px-4;
}

th {
  @apply text-left;
}

th.month-header {
  @apply text-right;
}

th.category-header {
  @apply font-light;
}

td {
  @apply font-light text-right;
}
</style>
