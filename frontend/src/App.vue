<script setup>
</script>

<template>
  <div class="flex flex-col h-full w-full">
    <div class="flex-none bg-red-500 flex flex-row">
      <span class="px-4 py-2 text-white text-lg font-bold">BudgetApp2</span>
      <RouterLink to="/">Start</RouterLink>
      <RouterLink to="/table">Tabelle</RouterLink>
      <RouterLink to="/import">Import</RouterLink>
    </div>
    <div class="p-4">
      <RouterView class="flex-1" />
    </div>
  </div>
</template>

<style scoped>
a {
  @apply px-4 py-2 text-white text-lg font-light
}

a:hover {
  @apply bg-red-400;
}
</style>
