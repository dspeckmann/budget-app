<script setup lang="ts">
import { XCircleIcon, XMarkIcon } from '@heroicons/vue/24/outline'

const model = defineModel<boolean>()
</script>

<template>
  <Teleport to="body" v-if="model">
    <div class="absolute left-0 top-0 w-full h-full z-50 flex items-center justify-center">
      <div class="modal bg-white shadow-sm rounded-md p-4 border">
        <div class="flex flex-row justify-between gap-4 w-full mb-8">
          <div class="text-xl">
            <slot name="title"></slot>
          </div>
          <XMarkIcon @click="$emit('update:modelValue', false)" class="close-icon w-7 h-7 text-xl cursor-pointer" />
        </div>
        <slot name="default"></slot>
      </div>
    </div>
  </Teleport>
</template>

<style scoped>
.close-icon:hover {
  @apply text-neutral-500;
}
</style>
