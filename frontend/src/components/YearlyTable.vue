<script setup lang="ts">
import { YearlyOverview } from '../domain/yearly-overview'
import { ref } from 'vue'
import { Transaction } from '../domain/transaction'
import TransactionDialog from './TransactionDialog.vue'

const overview: YearlyOverview = {
  year: 2024,
  accounts: [
    {
      account: 'Consorsbank',
      income: [],
      transfers: [],
      expenses: [],
    },
    {
      account: 'Trade Republic',
      income: [
        {
          name: 'Gehalt',
          category: 'Gehalt',
          amount: 1200,
          date: new Date(2024, 0, 1)
        },
        {
          name: 'Gehalt',
          category: 'Gehalt',
          amount: 1200,
          date: new Date(2024, 1, 1)
        },
        {
          name: 'Bonus',
          category: 'Gehalt',
          amount: 250,
          date: new Date(2024, 0, 1)
        }
      ],
      transfers: [],
      expenses: [],
    }
  ]
  // rows: [
  //   { category: 'Gehalt', account: 'Consorsbank', 0: 1200, 1: 1200, 2: 1200, 3: 1200, 4: 1200, 5: 1200, 6: 1200, 7: 1200, 8: 1200, 9: 1200, 10: 1200, 11: 1200, expenseCategory: 'Einnahmen' },
  //   { category: 'Miete', account: 'Consorsbank', 0: 1200, 1: 1200, 2: 1200, 3: 1200, 4: 1200, 5: 1200, 6: 1200, 7: 1200, 8: 1200, 9: 1200, 10: 1200, 11: 1200 , expenseCategory: 'Ausgaben' },
  //   { category: 'Reisen', account: 'Consorsbank', 0: 0, 1: 0, 2: 0, 3: 50.20, 4: 2400, 5: 0, 6: 0, 7: 12.80, 8: 99.99, 9: 0, 10: 0, 11: 0 , expenseCategory: 'Ausgaben' },
  //   { category: 'Lebensmittel', account: 'Consorsbank', 0: 100, 1: 90.5, 2: 82.33, 3: 75.22, 4: 120.23, 5: 200.98, 6: 182.73, 7: 88.90, 8: 111, 9: 120.12, 10: 187.5, 11: 105.3 , expenseCategory: 'Ausgaben' },
  //   { category: 'Lebensmittel', account: 'Trade Republic', 0: 0, 1: 0, 2: 0, 3: 30.4, 4: 45, 5: 43.99, 6: 22, 7: 0, 8: 99.99, 9: 56.70, 10: 43.2, 11: 20.5 , expenseCategory: 'Ausgaben' },
  // ]
}

const months = [
  { key: 1, title: 'Januar' },
  { key: 2, title: 'Februar' },
  { key: 3, title: 'März' },
  { key: 4, title: 'April' },
  { key: 5, title: 'Mai' },
  { key: 6, title: 'Juni' },
  { key: 7, title: 'Juli' },
  { key: 8, title: 'August' },
  { key: 9, title: 'September' },
  { key: 10, title: 'Oktober' },
  { key: 11, title: 'November' },
  { key: 12, title: 'Dezember' },
] as const

const transactionDialogVisible = ref(false)
const selectedMonth = ref(1)
const selectedCategory = ref('')

const showDialog = (month: number, category: string) => {
  selectedMonth.value = month
  selectedCategory.value = category
  transactionDialogVisible.value = true
}

const detailedExpenses = ref([
  { name: 'Edeka', amount: 20.55},
  { name: 'Rewe Center', amount: 500},
  { name: 'Aldi', amount: 31.4},
])

const onSubmit = () => {
  if (newExpense.value.amount !== undefined) {
    detailedExpenses.value.push({ name: 'Neue Ausgabe', amount: newExpense.value.amount })
  }
  newExpense.value = {}
}

const newExpense = ref<{
  amount?: number
}>({})

const collapsedAccounts = ref<string[]>([])

const toggleAccount = (account: string) => {
  if (collapsedAccounts.value.includes(account)) {
    collapsedAccounts.value = collapsedAccounts.value.filter(a => a !== account)
  } else {
    collapsedAccounts.value.push(account)
  }
}

const isAccountExpanded = (account: string) => {
  return !collapsedAccounts.value.includes(account)
}

const getCategories = (transactions: Transaction[]) => {
  const allCategories = transactions
    .map(t => t.category)
  return allCategories.filter((c, i) => allCategories.indexOf(c) === i)
}

const calculateAmount = (transactions: Transaction[], category: string, month: number, ) => {
  return transactions
    .filter(e => e.category === category && e.date.getMonth() + 1 === month)
    .reduce((acc, e) => acc + e.amount, 0)
}

const formatAmount = (amount: number) => {
  return new Intl.NumberFormat('de-DE', { style: 'currency', currency: 'EUR' }).format(
    amount,
  )
}
</script>

<template>
  <div class="border rounded-md shadow-sm overflow-x-auto">
    <table>
      <tr>
        <th></th>
        <th v-for="header in months" :key="header.key" class="month-header">{{ header.title }}</th>
      </tr>
      <template v-for="account in overview.accounts">
        <tr class="cursor-pointer bg-neutral-50" @click="toggleAccount(account.account)">
          <th colspan="13">{{ account.account }}</th>
        </tr>
        <template v-if="isAccountExpanded(account.account)">
        <tr>
          <th>Einnahmen</th>
        </tr>
        <tr v-for="category in getCategories(account.income)">
          <th class="category-header">{{ category }}</th>
          <td v-for="month in months" @click="showDialog(month.key, category)">
            {{ formatAmount(calculateAmount(account.income, category, month.key)) }}
          </td>
        </tr>
        <tr>
          <th>Umbuchungen</th>
        </tr>
        <tr v-for="category in getCategories(account.transfers)">
          <th class="category-header">{{ category }}</th>
          <td v-for="month in months" @click="showDialog(month.key, category)">
            {{ formatAmount(calculateAmount(account.transfers, category, month.key)) }}
          </td>
        </tr>
        <tr>
          <th>Ausgaben</th>
        </tr>
        <tr v-for="category in getCategories(account.expenses)">
          <th class="category-header">{{ category }}</th>
          <td v-for="month in months" @click="showDialog(month.key, category)">
            {{ formatAmount(calculateAmount(account.expenses, category, month.key)) }}
          </td>
        </tr>
      </template>
      </template>
    </table>
    <TransactionDialog v-model="transactionDialogVisible" :month="selectedMonth" :year="overview.year" :category="selectedCategory" />
  </div>
  <!-- <v-data-table :items="overview.rows" :headers="headers" :group-by="groupBy" density="compact" hide-default-footer disable-sort>
    <template #group-header="{ item, columns, toggleGroup, isGroupOpen }">
      <template :ref="(el) => { groupHeaders[item.value] = { item, toggleGroup, isGroupOpen } }" />
      <tr>
        <td :colspan="columns.length" @click="toggleGroup(item)" class="group-header">
          <v-icon>{{ isGroupOpen(item) ? '$expand' : '$next' }}</v-icon>
          {{ item.value }}
        </td>
      </tr>
    </template>
    <template #item="{ item, columns }">
      <tr>
        <th>{{ item.category }}</th>
        <td v-for="i in 12" @click="showDialog(columns[i - 1].title || 'Unbekannt', item.category)" style="cursor:pointer;" :class="{ 'total-row-item': item.isTotalRow }">
          {{ item[i - 1] }}
        </td>
      </tr>
    </template>
  </v-data-table>
  <v-dialog v-model="dialogVisible" style="max-width: 500px;">
    <v-card>
      <v-card-title>{{ dialogDetails.category }} im {{ dialogDetails.month }} {{ overview.year }}</v-card-title>
      <v-card-text>
        <v-form @submit.prevent="onSubmit">
          <v-text-field v-model="newExpense.amount" label="Neue Ausgabe..." autofocus></v-text-field>
          <v-btn>Hinzufügen</v-btn>
        </v-form>
        <v-data-table :items="detailedExpenses" :headers="detailsHeaders" density="compact" hide-default-footer></v-data-table>
      </v-card-text>
    </v-card>
  </v-dialog> -->
</template>

<style scoped>
td, th {
  /* 1 / 13 */
  width: 7.69%; 
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
  @apply font-light text-right cursor-pointer;
}

td:hover {
  @apply bg-neutral-50;
}
</style>
